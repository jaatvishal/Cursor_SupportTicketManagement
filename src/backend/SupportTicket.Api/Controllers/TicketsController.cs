using Microsoft.AspNetCore.Mvc;
using SupportTicket.Core.DTOs;
using SupportTicket.Core.Enums;
using SupportTicket.Core.Interfaces;
using SupportTicket.Core.Services;

namespace SupportTicket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TicketDto>>> GetTickets(
        [FromQuery] string? search,
        [FromQuery] string? status)
    {
        if (status is not null && !TicketStatus.All.Contains(status))
            return BadRequest(new ApiErrorResponse("Validation failed",
                [$"status: Invalid status value. Must be one of: {string.Join(", ", TicketStatus.All)}"]));

        var tickets = await _ticketService.GetTicketsAsync(new TicketQueryParams(search, status));
        return Ok(tickets);
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        return Ok(await _ticketService.GetUsersAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDetailDto>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket is null)
            return NotFound(new ApiErrorResponse($"Ticket with id {id} not found"));
        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> CreateTicket([FromBody] CreateTicketRequest request)
    {
        var errors = ValidateCreateRequest(request);
        if (errors.Count > 0)
            return BadRequest(new ApiErrorResponse("Validation failed", errors));

        try
        {
            var ticket = await _ticketService.CreateTicketAsync(request);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiErrorResponse("Validation failed", [ex.Message]));
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TicketDto>> UpdateTicket(int id, [FromBody] UpdateTicketRequest request)
    {
        var errors = ValidateUpdateRequest(request);
        if (errors.Count > 0)
            return BadRequest(new ApiErrorResponse("Validation failed", errors));

        try
        {
            var ticket = await _ticketService.UpdateTicketAsync(id, request);
            return Ok(ticket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new ApiErrorResponse(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiErrorResponse("Validation failed", [ex.Message]));
        }
    }

    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<TicketDto>> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Status))
            return BadRequest(new ApiErrorResponse("Validation failed", ["status: Status is required"]));

        if (!TicketStatus.All.Contains(request.Status))
            return BadRequest(new ApiErrorResponse("Validation failed",
                [$"status: Invalid status value. Must be one of: {string.Join(", ", TicketStatus.All)}"]));

        try
        {
            var ticket = await _ticketService.UpdateStatusAsync(id, request.Status);
            return Ok(ticket);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse(ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return UnprocessableEntity(new ApiErrorResponse(ex.Message));
        }
    }

    [HttpPost("{id:int}/comments")]
    public async Task<ActionResult<CommentDto>> AddComment(int id, [FromBody] CreateCommentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
            return BadRequest(new ApiErrorResponse("Validation failed", ["message: Comment message is required"]));

        if (request.CreatedBy <= 0)
            return BadRequest(new ApiErrorResponse("Validation failed", ["createdBy: CreatedBy must be a valid user ID"]));

        try
        {
            var comment = await _ticketService.AddCommentAsync(id, request);
            return CreatedAtAction(nameof(GetTicket), new { id }, comment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiErrorResponse(ex.Message));
        }
    }

    [HttpGet("transitions/{status}")]
    public ActionResult<IReadOnlyList<string>> GetAllowedTransitions(string status)
    {
        if (!TicketStatus.All.Contains(status))
            return BadRequest(new ApiErrorResponse("Validation failed", ["status: Invalid status value"]));

        return Ok(TicketStateMachine.GetAllowedTransitions(status));
    }

    private static List<string> ValidateCreateRequest(CreateTicketRequest request)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(request.Title))
            errors.Add("title: Title is required");
        else if (request.Title.Length > 200)
            errors.Add("title: Title must be 200 characters or less");

        if (string.IsNullOrWhiteSpace(request.Description))
            errors.Add("description: Description is required");

        if (string.IsNullOrWhiteSpace(request.Priority))
            errors.Add("priority: Priority is required");
        else if (!TicketPriority.All.Contains(request.Priority))
            errors.Add($"priority: Must be one of: {string.Join(", ", TicketPriority.All)}");

        if (request.CreatedBy <= 0)
            errors.Add("createdBy: CreatedBy must be a valid user ID");

        return errors;
    }

    private static List<string> ValidateUpdateRequest(UpdateTicketRequest request)
    {
        var errors = new List<string>();

        if (request.Title is not null && string.IsNullOrWhiteSpace(request.Title))
            errors.Add("title: Title cannot be empty");

        if (request.Description is not null && string.IsNullOrWhiteSpace(request.Description))
            errors.Add("description: Description cannot be empty");

        if (request.Priority is not null && !TicketPriority.All.Contains(request.Priority))
            errors.Add($"priority: Must be one of: {string.Join(", ", TicketPriority.All)}");

        if (request.Status is not null && !TicketStatus.All.Contains(request.Status))
            errors.Add($"status: Must be one of: {string.Join(", ", TicketStatus.All)}");

        return errors;
    }
}
