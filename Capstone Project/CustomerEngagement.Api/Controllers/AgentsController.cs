using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AgentsController : ControllerBase
{
    private readonly IAgentService _service;

    public AgentsController(IAgentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAgent(CreateAgentDto dto)
    {
        var id = await _service.CreateAgentAsync(dto.FullName, dto.Email);
        return Ok(new { AgentId = id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = false)
    {
        var agents = await _service.GetAllAgentsAsync(onlyActive);
        return Ok(agents);
    }

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        await _service.DeactivateAgentAsync(id);
        return Ok();
    }

    [HttpGet("workload/{id}")]
    public async Task<IActionResult> Workload(Guid id)
    {
        var count = await _service.GetAgentWorkloadAsync(id);
        return Ok(new { ActiveTickets = count });
    }
}