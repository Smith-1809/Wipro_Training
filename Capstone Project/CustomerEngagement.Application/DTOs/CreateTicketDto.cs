namespace CustomerEngagement.Application.DTOs;

public class CreateTicketDto
{
    public Guid CustomerId { get; set; }
    public Guid AgentId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}