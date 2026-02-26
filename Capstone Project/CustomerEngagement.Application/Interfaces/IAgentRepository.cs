using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Application.Interfaces;

public interface IAgentRepository
{
    Task AddAsync(Agent agent);
    Task<List<Agent>> GetAllAsync(bool onlyActive);
    Task DeactivateAsync(Guid agentId);
    Task<int> GetTicketCountAsync(Guid agentId);
}