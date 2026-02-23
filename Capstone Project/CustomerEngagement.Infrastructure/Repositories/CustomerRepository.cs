using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Application.DTOs;
using CustomerEngagement.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CustomerEngagement.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task AddAsync(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            @"INSERT INTO Customers (CustomerId, FullName, Email, Phone, CreatedAt)
              VALUES (@Id, @FullName, @Email, @Phone, @CreatedAt)", connection);

        command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = customer.Id;
        command.Parameters.Add("@FullName", SqlDbType.NVarChar, 150).Value = customer.FullName;
        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = customer.Email;
        command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = customer.Phone;
        command.Parameters.Add("@CreatedAt", SqlDbType.DateTime2).Value = customer.CreatedAt;

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            "SELECT * FROM Customers WHERE Email = @Email", connection);

        command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        if (!reader.Read())
            return null;

        return new Customer(
            reader["FullName"].ToString()!,
            reader["Email"].ToString()!,
            reader["Phone"].ToString()!
        );
    }

    // ✅ NEW METHOD
    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var customers = new List<CustomerDto>();

        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            "SELECT CustomerId, FullName, Email, Phone FROM Customers ORDER BY CreatedAt DESC",
            connection);

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            customers.Add(new CustomerDto
            {
                CustomerId = Guid.Parse(reader["CustomerId"].ToString()!),
                FullName = reader["FullName"].ToString()!,
                Email = reader["Email"].ToString()!,
                Phone = reader["Phone"].ToString()!
            });
        }

        return customers;
    }
}