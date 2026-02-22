using CustomerEngagement.Application.DTOs;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CustomerEngagement.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly string _connectionString;

    public TicketRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    // ============================
    // CREATE
    // ============================
    public async Task CreateAsync(
        Guid ticketId,
        Guid customerId,
        Guid agentId,
        int categoryId,
        string title,
        string description,
        int status,
        DateTime createdAt)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_CreateTicket", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@TicketId", SqlDbType.UniqueIdentifier).Value = ticketId;
        command.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier).Value = customerId;
        command.Parameters.Add("@AgentId", SqlDbType.UniqueIdentifier).Value = agentId;
        command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
        command.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = title;
        command.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = description;
        command.Parameters.Add("@Status", SqlDbType.Int).Value = status;
        command.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = createdAt;

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    // ============================
    // GET ALL
    // ============================
    public async Task<IEnumerable<TicketResponseDto>> GetAllAsync()
    {
        var tickets = new List<TicketResponseDto>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetAllTickets", connection);
        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tickets.Add(new TicketResponseDto
            {
                TicketId = (Guid)reader["TicketId"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                Status = (TicketStatus)(int)reader["Status"], // FIXED
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            });
        }

        return tickets;
    }

    // ============================
    // GET BY CUSTOMER
    // ============================
    public async Task<IEnumerable<TicketResponseDto>> GetByCustomerAsync(Guid customerId)
    {
        var tickets = new List<TicketResponseDto>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetTicketsByCustomer", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@CustomerId", SqlDbType.UniqueIdentifier).Value = customerId;

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tickets.Add(new TicketResponseDto
            {
                TicketId = (Guid)reader["TicketId"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                Status = (TicketStatus)(int)reader["Status"], // FIXED
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            });
        }

        return tickets;
    }

    // ============================
    // UPDATE
    // ============================
    public async Task UpdateAsync(
        Guid ticketId,
        string title,
        string description,
        int status,
        DateTime updatedAt)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_UpdateTicket", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@TicketId", SqlDbType.UniqueIdentifier).Value = ticketId;
        command.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = title;
        command.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = description;
        command.Parameters.Add("@Status", SqlDbType.Int).Value = status;
        command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime2).Value = updatedAt;

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    // ============================
    // RESOLVE
    // ============================
    public async Task ResolveAsync(Guid ticketId, int status, DateTime resolvedAt)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_ResolveTicket", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@TicketId", SqlDbType.UniqueIdentifier).Value = ticketId;
        command.Parameters.Add("@Status", SqlDbType.Int).Value = status;
        command.Parameters.Add("@ResolvedAt", SqlDbType.DateTime2).Value = resolvedAt;

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    // ============================
    // REPORT
    // ============================
    public async Task<IEnumerable<TicketReportDto>> GetStatusReportAsync()
    {
        var report = new List<TicketReportDto>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetTicketStatusReport", connection);
        command.CommandType = CommandType.StoredProcedure;

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            report.Add(new TicketReportDto
            {
                Status = (TicketStatus)(int)reader["Status"], // FIXED
                Total = (int)reader["Count"]                  // FIXED
            });
        }

        return report;
    }

    public async Task<IEnumerable<TicketResponseDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var tickets = new List<TicketResponseDto>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_GetTicketsPaged", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pageNumber;
        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            tickets.Add(new TicketResponseDto
            {
                TicketId = (Guid)reader["TicketId"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                Status = (TicketStatus)(int)reader["Status"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader["UpdatedAt"] as DateTime?
            });
        }

        return tickets;
    }
}