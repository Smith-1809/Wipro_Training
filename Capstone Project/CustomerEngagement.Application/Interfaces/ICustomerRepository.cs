using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Application.Interfaces;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task<Customer?> GetByEmailAsync(string email);
}