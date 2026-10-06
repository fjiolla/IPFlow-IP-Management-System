using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public interface ITrademarkRepository
{
    Task<IEnumerable<Trademark>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10);
    Task<Trademark?> GetByIdAsync(int trademarkId);
    Task<Trademark> CreateAsync(Trademark trademark);
    Task<Trademark?> UpdateAsync(int trademarkId, Trademark trademark);
    Task<bool> DeleteAsync(int trademarkId);
    Task<int> GetTotalCountAsync(string? status = null);
    Task<int> GetRenewalsDueCountAsync();
}
