using CustomerEngagement.Domain.Entities;
using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Application.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByEmailAsync(string email);

    // ✅ NEW
    Task<List<CustomerDto>> GetAllAsync();
}