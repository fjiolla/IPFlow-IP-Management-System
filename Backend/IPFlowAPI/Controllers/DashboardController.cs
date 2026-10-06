using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IPFlowAPI.Services;

namespace IPFlowAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard = await _dashboardService.GetDashboardDataAsync();
        return Ok(dashboard);
    }
}
