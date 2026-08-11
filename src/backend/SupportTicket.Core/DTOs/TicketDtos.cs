namespace SupportTicket.Core.DTOs;

public record TicketDto(
    int Id,
    string Title,
    string Description,
    string Priority,
    string Status,
    int? AssignedTo,
    int CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string? AssigneeName,
    string? CreatorName
);

public record CommentDto(
    int Id,
    int TicketId,
    string Message,
    int CreatedBy,
    DateTime CreatedAt,
    string? AuthorName
);

public record TicketDetailDto(
    int Id,
    string Title,
    string Description,
    string Priority,
    string Status,
    int? AssignedTo,
    int CreatedBy,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string? AssigneeName,
    string? CreatorName,
    List<CommentDto> Comments
);

public record UserDto(int Id, string Name, string Email, string Role);

public record CreateTicketRequest(
    string Title,
    string Description,
    string Priority,
    int? AssignedTo,
    int CreatedBy
);

public record UpdateTicketRequest(
    string? Title,
    string? Description,
    string? Priority,
    int? AssignedTo,
    string? Status
);

public record UpdateStatusRequest(string Status);

public record CreateCommentRequest(string Message, int CreatedBy);

public record TicketQueryParams(string? Search, string? Status);

public record ApiErrorResponse(string Error, List<string>? Details = null);
