using CustomerEngagement.Domain.Entities;

public interface IAgentService
{
    Task<Guid> CreateAgentAsync(string name, string email);
    Task<List<Agent>> GetAllAgentsAsync(bool onlyActive = false);
    Task DeactivateAgentAsync(Guid agentId);
    Task<int> GetAgentWorkloadAsync(Guid agentId);
}