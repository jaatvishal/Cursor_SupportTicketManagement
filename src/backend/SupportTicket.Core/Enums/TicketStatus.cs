namespace SupportTicket.Core.Enums;

public static class TicketStatus
{
    public const string Open = "Open";
    public const string InProgress = "In Progress";
    public const string Resolved = "Resolved";
    public const string Closed = "Closed";
    public const string Cancelled = "Cancelled";

    public static readonly string[] All = [Open, InProgress, Resolved, Closed, Cancelled];
}
