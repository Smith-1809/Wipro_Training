using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Application.Interfaces;

public interface ITicketRepository
{
    Task CreateAsync(Guid ticketId, Guid customerId, Guid agentId,
                     int categoryId, string title, string description,
                     int status, DateTime createdAt);

    Task<IEnumerable<TicketResponseDto>> GetAllAsync();

    Task<IEnumerable<TicketResponseDto>> GetByCustomerAsync(Guid customerId);

    Task UpdateAsync(Guid ticketId, string title, string description,
                     int status, DateTime updatedAt);

    Task ResolveAsync(Guid ticketId, int status, DateTime resolvedAt);

    Task<IEnumerable<TicketReportDto>> GetStatusReportAsync();

    Task<IEnumerable<TicketResponseDto>> GetPagedAsync(int pageNumber, int pageSize);
}