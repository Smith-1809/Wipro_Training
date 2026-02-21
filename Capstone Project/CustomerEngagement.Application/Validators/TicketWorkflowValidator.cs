using CustomerEngagement.Domain.Enums;          // REQUIRED for TicketStatus
using CustomerEngagement.Domain.Exceptions;     // REQUIRED for custom exception

namespace CustomerEngagement.Application.Validators;

/// <summary>
/// Validates ticket status transitions.
/// Ensures ticket lifecycle rules are enforced.
/// </summary>
public static class TicketWorkflowValidator
{
    /// <summary>
    /// Validates whether a transition from current → next is allowed.
    /// Throws exception if invalid.
    /// </summary>
    public static void ValidateTransition(TicketStatus current, TicketStatus next)
    {
        var validTransitions = new Dictionary<TicketStatus, List<TicketStatus>>
        {
            { TicketStatus.Open,       new List<TicketStatus> { TicketStatus.InProgress } },
            { TicketStatus.InProgress, new List<TicketStatus> { TicketStatus.Resolved } },
            { TicketStatus.Resolved,   new List<TicketStatus> { TicketStatus.Closed } }
        };

        // If no rule exists OR next state not allowed → throw exception
        if (!validTransitions.ContainsKey(current) ||
            !validTransitions[current].Contains(next))
        {
            throw new InvalidTicketStatusTransitionException(
                $"Invalid transition from {current} to {next}");
        }
    }
}