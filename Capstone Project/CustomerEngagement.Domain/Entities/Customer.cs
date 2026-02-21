namespace CustomerEngagement.Domain.Entities;

/// <summary>
/// Represents a customer in the system.
/// Pure domain model. No database logic.
/// </summary>
public class Customer
{
    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Customer(string fullName, string email, string phone)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        Phone = phone;
        CreatedAt = DateTime.UtcNow;
    }
}