using SupportTicket.Core.DTOs;

namespace SupportTicket.Core.Interfaces;

public interface ITicketService
{
    Task<List<TicketDto>> GetTicketsAsync(TicketQueryParams query);
    Task<TicketDetailDto?> GetTicketByIdAsync(int id);
    Task<TicketDto> CreateTicketAsync(CreateTicketRequest request);
    Task<TicketDto> UpdateTicketAsync(int id, UpdateTicketRequest request);
    Task<TicketDto> UpdateStatusAsync(int id, string newStatus);
    Task<CommentDto> AddCommentAsync(int ticketId, CreateCommentRequest request);
    Task<List<UserDto>> GetUsersAsync();
}
