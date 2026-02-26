using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Entities;
public class AgentService : IAgentService
{
    private readonly IAgentRepository _repository;

    public AgentService(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAgentAsync(string name, string email)
    {
        var agent = new Agent
        {
            AgentId = Guid.NewGuid(),
            FullName = name,
            Email = email,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(agent);
        return agent.AgentId;
    }

    public async Task<List<Agent>> GetAllAgentsAsync(bool onlyActive = false)
    {
        return await _repository.GetAllAsync(onlyActive);
    }

    public async Task DeactivateAgentAsync(Guid agentId)
    {
        await _repository.DeactivateAsync(agentId);
    }

    public async Task<int> GetAgentWorkloadAsync(Guid agentId)
    {
        return await _repository.GetTicketCountAsync(agentId);
    }
}