using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPFlowAPI.DTOs;
using IPFlowAPI.Services;

namespace IPFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CasesController : ControllerBase
{
    private readonly ICaseService _caseService;

    public CasesController(ICaseService caseService)
    {
        _caseService = caseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int? clientId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (cases, totalCount) = await _caseService.GetAllAsync(status, clientId, pageNumber, pageSize);
        return Ok(new { cases, totalCount, pageNumber, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var caseEntity = await _caseService.GetByIdAsync(id);
        if (caseEntity == null)
        {
            return NotFound(new { message = "Case not found" });
        }

        return Ok(caseEntity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCaseDTO dto)
    {
        var caseEntity = await _caseService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = caseEntity.CaseId }, caseEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCaseDTO dto)
    {
        var caseEntity = await _caseService.UpdateAsync(id, dto);
        if (caseEntity == null)
        {
            return NotFound(new { message = "Case not found" });
        }

        return Ok(caseEntity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _caseService.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { message = "Case not found" });
        }

        return NoContent();
    }
}
