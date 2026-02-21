using CustomerEngagement.Domain.Entities;
using CustomerEngagement.Domain.Enums;

namespace CustomerEngagement.Application.Interfaces;

public interface ITicketService
{
    Task<Guid> CreateTicketAsync(
        Guid customerId,
        Guid agentId,
        int categoryId,
        string title,
        string description);

    Task<Ticket?> GetTicketByIdAsync(Guid id);

    Task UpdateTicketStatusAsync(Guid id, TicketStatus status);
}