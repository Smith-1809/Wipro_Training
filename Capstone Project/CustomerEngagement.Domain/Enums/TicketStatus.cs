namespace CustomerEngagement.Domain.Enums;

/// <summary>
/// Ticket lifecycle state.
/// Stored as INT in database.
/// </summary>
public enum TicketStatus
{
    Open = 0,
    InProgress = 1,
    Resolved = 2,
    Closed = 3
}