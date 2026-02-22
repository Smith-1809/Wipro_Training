using Microsoft.AspNetCore.Mvc;
using CustomerEngagement.Application.Interfaces;
using CustomerEngagement.Application.DTOs;

namespace CustomerEngagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(
        ITicketService ticketService,
        ILogger<TicketsController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    // ==============================
    // CREATE
    // ==============================
    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketDto dto)
    {
      
        _logger.LogInformation("Creating ticket for CustomerId: {CustomerId}", dto.CustomerId);

        var ticketId = await _ticketService.CreateAsync(dto);

        _logger.LogInformation("Ticket created successfully with Id: {TicketId}", ticketId);

        return CreatedAtAction(nameof(GetById), new { id = ticketId }, new { ticketId });
    }

    // ==============================
    // GET BY ID
    // ==============================
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Fetching ticket by Id: {TicketId}", id);

        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            _logger.LogWarning("Ticket not found: {TicketId}", id);
            return NotFound();
        }

        return Ok(ticket);
    }

    // ==============================
    // GET ALL (PAGINATION)
    // ==============================
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        _logger.LogInformation("Fetching tickets page {PageNumber} with size {PageSize}", pageNumber, pageSize);

        var tickets = await _ticketService.GetAllAsync(pageNumber, pageSize);

        return Ok(tickets);
    }

    // ==============================
    // UPDATE
    // ==============================
    [HttpPut]
    public async Task<IActionResult> Update(UpdateTicketDto dto)
    {
        _logger.LogInformation("Updating ticket {TicketId}", dto.TicketId);

        await _ticketService.UpdateAsync(dto);

        _logger.LogInformation("Ticket updated successfully: {TicketId}", dto.TicketId);

        return Ok("Ticket updated.");
    }

    // ==============================
    // RESOLVE
    // ==============================
    [HttpPut("resolve/{ticketId:guid}")]
    public async Task<IActionResult> Resolve(Guid ticketId)
    {
        _logger.LogInformation("Resolving ticket: {TicketId}", ticketId);

        await _ticketService.ResolveAsync(ticketId);

        _logger.LogInformation("Ticket resolved successfully: {TicketId}", ticketId);

        return Ok("Ticket resolved.");
    }

    // ==============================
    // REPORT
    // ==============================
    [HttpGet("report/status")]
    public async Task<IActionResult> GetStatusReport()
    {
        _logger.LogInformation("Fetching ticket status report");

        var report = await _ticketService.GetStatusReportAsync();

        return Ok(report);
    }
}