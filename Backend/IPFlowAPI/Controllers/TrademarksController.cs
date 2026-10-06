using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPFlowAPI.DTOs;
using IPFlowAPI.Services;

namespace IPFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TrademarksController : ControllerBase
{
    private readonly ITrademarkService _trademarkService;

    public TrademarksController(ITrademarkService trademarkService)
    {
        _trademarkService = trademarkService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int? clientId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (trademarks, totalCount) = await _trademarkService.GetAllAsync(status, clientId, pageNumber, pageSize);
        return Ok(new { trademarks, totalCount, pageNumber, pageSize });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trademark = await _trademarkService.GetByIdAsync(id);
        if (trademark == null)
        {
            return NotFound(new { message = "Trademark not found" });
        }

        return Ok(trademark);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTrademarkDTO dto)
    {
        var trademark = await _trademarkService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = trademark.TrademarkId }, trademark);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTrademarkDTO dto)
    {
        var trademark = await _trademarkService.UpdateAsync(id, dto);
        if (trademark == null)
        {
            return NotFound(new { message = "Trademark not found" });
        }

        return Ok(trademark);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _trademarkService.DeleteAsync(id);
        if (!result)
        {
            return NotFound(new { message = "Trademark not found" });
        }

        return NoContent();
    }
}
