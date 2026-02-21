using CustomerEngagement.Domain.Enums;

namespace CustomerEngagement.Domain.Entities;

/// <summary>
/// Represents a support ticket.
/// </summary>
public class Ticket
{
    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid AgentId { get; private set; }

    public int CategoryId { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public TicketStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public DateTime? ResolvedAt { get; private set; }

    public Ticket(Guid customerId, Guid agentId, int categoryId,
                  string title, string description)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        AgentId = agentId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Status = TicketStatus.Open;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TicketStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;

        if (newStatus == TicketStatus.Resolved)
            ResolvedAt = DateTime.UtcNow;
    }
}