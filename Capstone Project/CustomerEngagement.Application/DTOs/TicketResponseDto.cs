using CustomerEngagement.Domain.Enums;

namespace CustomerEngagement.Application.DTOs;

public class TicketResponseDto
{
    public Guid TicketId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // IMPORTANT: Strongly typed enum
    public TicketStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}