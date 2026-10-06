using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface IDashboardService
{
    Task<DashboardDTO> GetDashboardDataAsync();
}
