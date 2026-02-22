using CustomerEngagement.Application.DTOs;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Domain.Enums;
using System.Linq;

namespace CustomerEngagement.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository)
    {
        _repository = repository;
    }

    // ==============================
    // CREATE
    // ==============================
    public async Task<Guid> CreateAsync(CreateTicketDto dto)
    {
        var ticketId = Guid.NewGuid();

        await _repository.CreateAsync(
            ticketId,
            dto.CustomerId,
            dto.AgentId,
            dto.CategoryId,
            dto.Title,
            dto.Description,
            (int)TicketStatus.Open,
            DateTime.UtcNow
        );

        return ticketId;
    }

    // ==============================
    // GET BY ID
    // ==============================
    public async Task<TicketResponseDto?> GetByIdAsync(Guid id)
    {
        var all = await _repository.GetAllAsync();
        return all.FirstOrDefault(t => t.TicketId == id);
    }

    // ==============================
    // GET ALL (PAGINATION)
    // ==============================
    public async Task<IEnumerable<TicketResponseDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetPagedAsync(pageNumber, pageSize);
    }

    // ==============================
    // GET BY CUSTOMER
    // ==============================
    public async Task<IEnumerable<TicketResponseDto>> GetByCustomerAsync(Guid customerId)
    {
        return await _repository.GetByCustomerAsync(customerId);
    }

    // ==============================
    // UPDATE
    // ==============================
    public async Task UpdateAsync(UpdateTicketDto dto)
    {
        await _repository.UpdateAsync(
            dto.TicketId,
            dto.Title,
            dto.Description,
            dto.Status,
            DateTime.UtcNow
        );
    }

    // ==============================
    // RESOLVE
    // ==============================
    public async Task ResolveAsync(Guid ticketId)
    {
        await _repository.ResolveAsync(
            ticketId,
            (int)TicketStatus.Resolved,
            DateTime.UtcNow
        );
    }

    // ==============================
    // REPORT
    // ==============================
    public async Task<IEnumerable<TicketReportDto>> GetStatusReportAsync()
    {
        return await _repository.GetStatusReportAsync();
    }
}