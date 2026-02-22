using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Application.Interfaces;

public interface ITicketService
{
    // Create
    Task<Guid> CreateAsync(CreateTicketDto dto);

    // Get single
    Task<TicketResponseDto?> GetByIdAsync(Guid id);

    // Get paginated
    Task<IEnumerable<TicketResponseDto>> GetAllAsync(int pageNumber, int pageSize);

    // Get by customer
    Task<IEnumerable<TicketResponseDto>> GetByCustomerAsync(Guid customerId);

    // Update
    Task UpdateAsync(UpdateTicketDto dto);

    // Resolve
    Task ResolveAsync(Guid ticketId);

    // Report
    Task<IEnumerable<TicketReportDto>> GetStatusReportAsync();
}