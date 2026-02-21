// Required for API controller features
using Microsoft.AspNetCore.Mvc;

// Required for IConfiguration (connection string access)
using Microsoft.Extensions.Configuration;

// Required for SQL connection test
using Microsoft.Data.SqlClient;
using System.Data;

// Application layer service interface
using CustomerEngagement.Application.Interfaces;

// DTO for creating customer
using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Api.Controllers;

/// <summary>
/// Handles HTTP requests related to Customers
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor - Dependencies injected automatically by ASP.NET Core
    /// </summary>
    public CustomersController(
        ICustomerService customerService,
        IConfiguration configuration)
    {
        _customerService = customerService;
        _configuration = configuration;
    }

    /// <summary>
    /// Creates a new customer
    /// Calls service layer which calls repository (stored procedure)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        try
        {
            // Validate input (basic check)
            if (string.IsNullOrWhiteSpace(dto.FullName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest("FullName and Email are required.");
            }

            // Call service layer
            var customerId = await _customerService
                .CreateCustomerAsync(dto.FullName, dto.Email, dto.Phone);

            // Return 201 Created with new ID
            return CreatedAtAction(
                nameof(CreateCustomer),
                new { id = customerId },
                new { CustomerId = customerId });
        }
        catch (Exception ex)
        {
            // Return 500 if something unexpected happens
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Temporary endpoint to verify database connection
    /// REMOVE after testing
    /// </summary>
    [HttpGet("test-db")]
    public async Task<IActionResult> TestDatabaseConnection()
    {
        try
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            return Ok($"Database Connection Successful. State: {connection.State}");
        }
        catch (Exception ex)
        {
            return BadRequest($"Database Connection Failed: {ex.Message}");
        }
    }
}