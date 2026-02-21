using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _connectionString;

    public CustomerRepository(IConfiguration configuration)
    {
        _connectionString =
    configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(_connectionString))
        {
            Console.WriteLine("Connection string is NULL!");
        }
    }

    public async Task AddAsync(Customer customer)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            @"INSERT INTO Customers (CustomerId, FullName, Email, Phone, CreatedAt)
              VALUES (@Id, @FullName, @Email, @Phone, @CreatedAt)", connection);

        command.Parameters.AddWithValue("@Id", customer.Id);
        command.Parameters.AddWithValue("@FullName", customer.FullName);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@Phone", customer.Phone);
        command.Parameters.AddWithValue("@CreatedAt", customer.CreatedAt);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        using var connection = new SqlConnection(_connectionString);

        using var command = new SqlCommand(
            "SELECT * FROM Customers WHERE Email = @Email", connection);

        command.Parameters.AddWithValue("@Email", email);

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
}