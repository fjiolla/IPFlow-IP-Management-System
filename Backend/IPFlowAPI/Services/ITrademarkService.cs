using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface ITrademarkService
{
    Task<(IEnumerable<TrademarkDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize);
    Task<TrademarkDTO?> GetByIdAsync(int trademarkId);
    Task<TrademarkDTO> CreateAsync(CreateTrademarkDTO dto);
    Task<TrademarkDTO?> UpdateAsync(int trademarkId, UpdateTrademarkDTO dto);
    Task<bool> DeleteAsync(int trademarkId);
}
