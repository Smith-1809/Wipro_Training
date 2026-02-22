using CustomerEngagement.Domain.Enums;

namespace CustomerEngagement.Application.DTOs;

public class TicketReportDto
{
    public TicketStatus Status { get; set; }

    public int Total { get; set; }
}