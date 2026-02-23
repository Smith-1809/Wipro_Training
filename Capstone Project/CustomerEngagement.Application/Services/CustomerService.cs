using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Application.DTOs;
using CustomerEngagement.Domain.Entities;

namespace CustomerEngagement.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateCustomerAsync(string name, string email, string phone)
    {
        var existing = await _repository.GetByEmailAsync(email);

        if (existing != null)
            throw new Exception("Customer already exists.");

        var customer = new Customer(name, email, phone);

        await _repository.AddAsync(customer);

        return customer.Id;
    }

    // ✅ NEW METHOD
    public async Task<List<CustomerDto>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }
}