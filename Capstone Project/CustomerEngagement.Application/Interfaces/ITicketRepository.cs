using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Application.Interfaces;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket);
    Task<Ticket?> GetByIdAsync(Guid id);
    Task UpdateAsync(Ticket ticket);
    Task<bool> ExistsDuplicateAsync(Guid customerId, string title);
}