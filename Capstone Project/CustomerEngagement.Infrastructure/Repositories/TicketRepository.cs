using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly string _connectionString;

    public TicketRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }

    public async Task AddAsync(Ticket ticket)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            @"INSERT INTO Tickets
              (TicketId, CustomerId, AgentId, CategoryId, Title,
               Description, Status, CreatedAt)
              VALUES
              (@Id, @CustomerId, @AgentId, @CategoryId,
               @Title, @Description, @Status, @CreatedAt)",
            connection);

        command.Parameters.AddWithValue("@Id", ticket.Id);
        command.Parameters.AddWithValue("@CustomerId", ticket.CustomerId);
        command.Parameters.AddWithValue("@AgentId", ticket.AgentId);
        command.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
        command.Parameters.AddWithValue("@Title", ticket.Title);
        command.Parameters.AddWithValue("@Description", ticket.Description);
        command.Parameters.AddWithValue("@Status", (int)ticket.Status);
        command.Parameters.AddWithValue("@CreatedAt", ticket.CreatedAt);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            "SELECT * FROM Tickets WHERE TicketId = @Id", connection);

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (!reader.Read())
            return null;

        return new Ticket(
            (Guid)reader["CustomerId"],
            (Guid)reader["AgentId"],
            (int)reader["CategoryId"],
            reader["Title"].ToString()!,
            reader["Description"].ToString()!
        );
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            @"UPDATE Tickets
              SET Status = @Status,
                  UpdatedAt = @UpdatedAt,
                  ResolvedAt = @ResolvedAt
              WHERE TicketId = @Id",
            connection);

        command.Parameters.AddWithValue("@Id", ticket.Id);
        command.Parameters.AddWithValue("@Status", (int)ticket.Status);
        command.Parameters.AddWithValue("@UpdatedAt", ticket.UpdatedAt ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ResolvedAt", ticket.ResolvedAt ?? (object)DBNull.Value);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<bool> ExistsDuplicateAsync(Guid customerId, string title)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            @"SELECT COUNT(1)
              FROM Tickets
              WHERE CustomerId = @CustomerId
              AND Title = @Title",
            connection);

        command.Parameters.AddWithValue("@CustomerId", customerId);
        command.Parameters.AddWithValue("@Title", title);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        int count = result != null && result != DBNull.Value
            ? Convert.ToInt32(result)
            : 0;

        return count > 0;
    }
}