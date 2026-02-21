using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Entities;
using CustomerEngagement.Domain.Enums;

namespace CustomerEngagement.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateTicketAsync(
        Guid customerId,
        Guid agentId,
        int categoryId,
        string title,
        string description)
    {
        var duplicate = await _repository.ExistsDuplicateAsync(customerId, title);

        if (duplicate)
            throw new Exception("Duplicate ticket.");

        var ticket = new Ticket(customerId, agentId, categoryId, title, description);

        await _repository.AddAsync(ticket);

        return ticket.Id;
    }

    public async Task<Ticket?> GetTicketByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateTicketStatusAsync(Guid id, TicketStatus status)
    {
        var ticket = await _repository.GetByIdAsync(id);

        if (ticket == null)
            throw new Exception("Ticket not found.");

        ticket.UpdateStatus(status);

        await _repository.UpdateAsync(ticket);
    }
}