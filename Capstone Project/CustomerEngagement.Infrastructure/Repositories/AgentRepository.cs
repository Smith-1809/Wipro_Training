using Microsoft.Data.SqlClient;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Entities;
using Microsoft.Extensions.Configuration;

public class AgentRepository : IAgentRepository
{
    private readonly string _connectionString;

    public AgentRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task AddAsync(Agent agent)
    {
        using var connection = new SqlConnection(_connectionString);

        var command = new SqlCommand(@"
            INSERT INTO Agents (AgentId, FullName, Email, IsActive, CreatedAt)
            VALUES (@Id, @Name, @Email, @IsActive, @CreatedAt)", connection);

        command.Parameters.AddWithValue("@Id", agent.AgentId);
        command.Parameters.AddWithValue("@Name", agent.FullName);
        command.Parameters.AddWithValue("@Email", agent.Email);
        command.Parameters.AddWithValue("@IsActive", agent.IsActive);
        command.Parameters.AddWithValue("@CreatedAt", agent.CreatedAt);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Agent>> GetAllAsync(bool onlyActive)
    {
        var agents = new List<Agent>();

        using var connection = new SqlConnection(_connectionString);

        var query = onlyActive
            ? "SELECT * FROM Agents WHERE IsActive = 1"
            : "SELECT * FROM Agents";

        var command = new SqlCommand(query, connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            agents.Add(new Agent
            {
                AgentId = (Guid)reader["AgentId"],
                FullName = reader["FullName"].ToString()!,
                Email = reader["Email"].ToString()!,
                IsActive = (bool)reader["IsActive"],
                CreatedAt = (DateTime)reader["CreatedAt"]
            });
        }

        return agents;
    }

    public async Task DeactivateAsync(Guid agentId)
    {
        using var connection = new SqlConnection(_connectionString);

        var command = new SqlCommand(@"
            UPDATE Agents
            SET IsActive = 0
            WHERE AgentId = @Id", connection);

        command.Parameters.AddWithValue("@Id", agentId);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<int> GetTicketCountAsync(Guid agentId)
    {
        using var connection = new SqlConnection(_connectionString);

        var command = new SqlCommand(@"
            SELECT COUNT(*) FROM Tickets
            WHERE AgentId = @Id AND Status != 2", connection);

        command.Parameters.AddWithValue("@Id", agentId);

        await connection.OpenAsync();
        return (int)await command.ExecuteScalarAsync();
    }
}