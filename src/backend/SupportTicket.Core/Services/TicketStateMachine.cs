namespace SupportTicket.Core.Services;

/// <summary>
/// Enforced ticket status state machine.
/// Valid transitions:
///   Open         -> In Progress
///   In Progress  -> Resolved
///   Resolved     -> Closed
///   Open         -> Cancelled
///   In Progress  -> Cancelled
/// </summary>
public static class TicketStateMachine
{
  private static readonly Dictionary<string, string[]> ValidTransitions = new()
  {
    [Enums.TicketStatus.Open] = [Enums.TicketStatus.InProgress, Enums.TicketStatus.Cancelled],
    [Enums.TicketStatus.InProgress] = [Enums.TicketStatus.Resolved, Enums.TicketStatus.Cancelled],
    [Enums.TicketStatus.Resolved] = [Enums.TicketStatus.Closed],
    [Enums.TicketStatus.Closed] = [],
    [Enums.TicketStatus.Cancelled] = [],
  };

  public static bool IsValidTransition(string currentStatus, string newStatus)
  {
    if (currentStatus == newStatus) return true;
    return ValidTransitions.TryGetValue(currentStatus, out var allowed) && allowed.Contains(newStatus);
  }

  public static IReadOnlyList<string> GetAllowedTransitions(string status)
  {
    return ValidTransitions.TryGetValue(status, out var allowed) ? allowed : [];
  }

  public static (bool Valid, string? Message) ValidateTransition(string currentStatus, string newStatus)
  {
    if (currentStatus == newStatus) return (true, null);

    if (!IsValidTransition(currentStatus, newStatus))
    {
      var allowed = GetAllowedTransitions(currentStatus);
      var allowedStr = allowed.Count > 0 ? string.Join(", ", allowed) : "none (terminal state)";
      return (false, $"Invalid status transition from '{currentStatus}' to '{newStatus}'. Allowed transitions: {allowedStr}");
    }

    return (true, null);
  }
}
