namespace CustomerEngagement.Application.DTOs;

/// <summary>
/// DTO used when creating a new customer from API request
/// </summary>
public class CreateCustomerDto
{
    /// <summary>
    /// Customer full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Customer email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Customer phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}