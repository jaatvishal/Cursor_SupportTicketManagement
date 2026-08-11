namespace SupportTicket.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public ICollection<Ticket> AssignedTickets { get; set; } = [];
    public ICollection<Ticket> CreatedTickets { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
}
