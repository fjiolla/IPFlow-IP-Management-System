using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPFlowAPI.DTOs;
using IPFlowAPI.Services;

namespace IPFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (clients, totalCount) = await _clientService.GetAllAsync(pageNumber, pageSize);
        return Ok(new { clients, totalCount, pageNumber, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
        {
            return NotFound(new { message = "Client not found" });
        }

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDTO dto)
    {
        var client = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = client.ClientId }, client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClientDTO dto)
    {
        var client = await _clientService.UpdateAsync(id, dto);
        if (client == null)
        {
            return NotFound(new { message = "Client not found" });
        }

        return Ok(client);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clientService.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { message = "Client not found" });
        }

        return NoContent();
    }
}
