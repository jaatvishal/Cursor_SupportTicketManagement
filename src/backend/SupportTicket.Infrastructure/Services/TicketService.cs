using Microsoft.EntityFrameworkCore;
using SupportTicket.Core.DTOs;
using SupportTicket.Core.Entities;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Interfaces;
using SupportTicket.Core.Services;
using SupportTicket.Infrastructure.Data;

namespace SupportTicket.Infrastructure.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _db;

    public TicketService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TicketDto>> GetTicketsAsync(TicketQueryParams query)
    {
        var q = _db.Tickets
            .Include(t => t.Assignee)
            .Include(t => t.Creator)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Status))
            q = q.Where(t => t.Status == query.Status);

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var term = query.Search.Trim();
            q = q.Where(t => t.Title.Contains(term) || t.Description.Contains(term));
        }

        var tickets = await q.OrderByDescending(t => t.UpdatedAt).ToListAsync();
        return tickets.Select(MapToDto).ToList();
    }

    public async Task<TicketDetailDto?> GetTicketByIdAsync(int id)
    {
        var ticket = await _db.Tickets
            .Include(t => t.Assignee)
            .Include(t => t.Creator)
            .Include(t => t.Comments.OrderBy(c => c.CreatedAt))
                .ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null) return null;

        return new TicketDetailDto(
            ticket.Id, ticket.Title, ticket.Description, ticket.Priority, ticket.Status,
            ticket.AssignedTo, ticket.CreatedBy, ticket.CreatedAt, ticket.UpdatedAt,
            ticket.Assignee?.Name, ticket.Creator.Name,
            ticket.Comments.Select(c => new CommentDto(
                c.Id, c.TicketId, c.Message, c.CreatedBy, c.CreatedAt, c.Author.Name
            )).ToList()
        );
    }

    public async Task<TicketDto> CreateTicketAsync(CreateTicketRequest request)
    {
        await ValidateUserExists(request.CreatedBy, "CreatedBy");
        if (request.AssignedTo.HasValue)
            await ValidateUserExists(request.AssignedTo.Value, "AssignedTo");

        ValidatePriority(request.Priority);

        var ticket = new Ticket
        {
            Title = request.Title.Trim(),
            Description = request.Description.Trim(),
            Priority = request.Priority,
            AssignedTo = request.AssignedTo,
            CreatedBy = request.CreatedBy,
            Status = TicketStatus.Open,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        return (await GetTicketByIdAsync(ticket.Id))! is TicketDetailDto detail
            ? MapToDto(detail)
            : throw new InvalidOperationException("Failed to retrieve created ticket");
    }

    public async Task<TicketDto> UpdateTicketAsync(int id, UpdateTicketRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(id)
            ?? throw new KeyNotFoundException($"Ticket with id {id} not found");

        if (request.Title is not null)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be empty");
            ticket.Title = request.Title.Trim();
        }

        if (request.Description is not null)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
                throw new ArgumentException("Description cannot be empty");
            ticket.Description = request.Description.Trim();
        }

        if (request.Priority is not null)
        {
            ValidatePriority(request.Priority);
            ticket.Priority = request.Priority;
        }

        if (request.AssignedTo.HasValue)
            await ValidateUserExists(request.AssignedTo.Value, "AssignedTo");
        if (request.AssignedTo is not null)
            ticket.AssignedTo = request.AssignedTo;

        if (request.Status is not null && request.Status != ticket.Status)
        {
            var validation = TicketStateMachine.ValidateTransition(ticket.Status, request.Status);
            if (!validation.Valid)
                throw new InvalidOperationException(validation.Message);
            ticket.Status = request.Status;
        }

        ticket.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var updated = await GetTicketByIdAsync(id);
        return MapToDto(updated!);
    }

    public async Task<TicketDto> UpdateStatusAsync(int id, string newStatus)
    {
        var ticket = await _db.Tickets.FindAsync(id)
            ?? throw new KeyNotFoundException($"Ticket with id {id} not found");

        if (!TicketStatus.All.Contains(newStatus))
            throw new ArgumentException($"Invalid status value: {newStatus}");

        var validation = TicketStateMachine.ValidateTransition(ticket.Status, newStatus);
        if (!validation.Valid)
            throw new InvalidOperationException(validation.Message);

        ticket.Status = newStatus;
        ticket.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var updated = await GetTicketByIdAsync(id);
        return MapToDto(updated!);
    }

    public async Task<CommentDto> AddCommentAsync(int ticketId, CreateCommentRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId)
            ?? throw new KeyNotFoundException($"Ticket with id {ticketId} not found");

        await ValidateUserExists(request.CreatedBy, "CreatedBy");

        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Comment message is required");

        var comment = new Comment
        {
            TicketId = ticketId,
            Message = request.Message.Trim(),
            CreatedBy = request.CreatedBy,
            CreatedAt = DateTime.UtcNow,
        };

        _db.Comments.Add(comment);
        ticket.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        var author = await _db.Users.FindAsync(request.CreatedBy);
        return new CommentDto(comment.Id, comment.TicketId, comment.Message,
            comment.CreatedBy, comment.CreatedAt, author?.Name);
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _db.Users
            .OrderBy(u => u.Name)
            .Select(u => new UserDto(u.Id, u.Name, u.Email, u.Role))
            .ToListAsync();
    }

    private async Task ValidateUserExists(int userId, string fieldName)
    {
        if (!await _db.Users.AnyAsync(u => u.Id == userId))
            throw new KeyNotFoundException($"User with id {userId} not found ({fieldName})");
    }

    private static void ValidatePriority(string priority)
    {
        if (!TicketPriority.All.Contains(priority))
            throw new ArgumentException($"Priority must be one of: {string.Join(", ", TicketPriority.All)}");
    }

    private static TicketDto MapToDto(Ticket ticket) => new(
        ticket.Id, ticket.Title, ticket.Description, ticket.Priority, ticket.Status,
        ticket.AssignedTo, ticket.CreatedBy, ticket.CreatedAt, ticket.UpdatedAt,
        ticket.Assignee?.Name, ticket.Creator?.Name
    );

    private static TicketDto MapToDto(TicketDetailDto detail) => new(
        detail.Id, detail.Title, detail.Description, detail.Priority, detail.Status,
        detail.AssignedTo, detail.CreatedBy, detail.CreatedAt, detail.UpdatedAt,
        detail.AssigneeName, detail.CreatorName
    );
}
