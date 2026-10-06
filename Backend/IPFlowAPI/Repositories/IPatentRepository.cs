using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public interface IPatentRepository
{
    Task<IEnumerable<Patent>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10);
    Task<Patent?> GetByIdAsync(int patentId);
    Task<Patent> CreateAsync(Patent patent);
    Task<Patent?> UpdateAsync(int patentId, Patent patent);
    Task<bool> DeleteAsync(int patentId);
    Task<int> GetTotalCountAsync(string? status = null);
}
