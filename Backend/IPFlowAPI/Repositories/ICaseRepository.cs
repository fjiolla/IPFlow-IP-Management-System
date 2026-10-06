using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public interface ICaseRepository
{
    Task<IEnumerable<Case>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10);
    Task<Case?> GetByIdAsync(int caseId);
    Task<Case> CreateAsync(Case caseEntity);
    Task<Case?> UpdateAsync(int caseId, Case caseEntity);
    Task<bool> DeleteAsync(int caseId);
    Task<int> GetTotalCountAsync(string? status = null);
}
