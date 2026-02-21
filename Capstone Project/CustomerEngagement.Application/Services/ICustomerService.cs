namespace CustomerEngagement.Application.Interfaces;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(string name, string email, string phone);
}