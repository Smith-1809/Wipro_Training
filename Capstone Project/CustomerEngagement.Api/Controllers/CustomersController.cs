using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IConfiguration _configuration;

    public CustomersController(
        ICustomerService customerService,
        IConfiguration configuration)
    {
        _customerService = customerService;
        _configuration = configuration;
    }

    // ===============================
    // CREATE CUSTOMER
    // ===============================
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.FullName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest("FullName and Email are required.");
            }

            var customerId = await _customerService
                .CreateCustomerAsync(dto.FullName, dto.Email, dto.Phone);

            return CreatedAtAction(
                nameof(GetAllCustomers),
                new { id = customerId },
                new { customerId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    // ===============================
    // GET ALL CUSTOMERS  ✅ NEW
    // ===============================
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        try
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    // ===============================
    // TEST DATABASE CONNECTION
    // ===============================
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