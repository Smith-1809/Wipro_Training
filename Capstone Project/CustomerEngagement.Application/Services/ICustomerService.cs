namespace CustomerEngagement.Application.Interfaces;

using CustomerEngagement.Application.DTOs;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(string name, string email, string phone);

    // ✅ NEW
    Task<List<CustomerDto>> GetAllCustomersAsync();
}