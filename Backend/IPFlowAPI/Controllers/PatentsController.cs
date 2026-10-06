using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPFlowAPI.DTOs;
using IPFlowAPI.Services;

namespace IPFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatentsController : ControllerBase
{
    private readonly IPatentService _patentService;

    public PatentsController(IPatentService patentService)
    {
        _patentService = patentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int? clientId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (patents, totalCount) = await _patentService.GetAllAsync(status, clientId, pageNumber, pageSize);
        return Ok(new { patents, totalCount, pageNumber, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patent = await _patentService.GetByIdAsync(id);
        if (patent == null)
        {
            return NotFound(new { message = "Patent not found" });
        }

        return Ok(patent);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatentDTO dto)
    {
        var patent = await _patentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = patent.PatentId }, patent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatentDTO dto)
    {
        var patent = await _patentService.UpdateAsync(id, dto);
        if (patent == null)
        {
            return NotFound(new { message = "Patent not found" });
        }

        return Ok(patent);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _patentService.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { message = "Patent not found" });
        }

        return NoContent();
    }
}
