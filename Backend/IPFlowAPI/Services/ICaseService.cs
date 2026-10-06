using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface ICaseService
{
    Task<(IEnumerable<CaseDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize);
    Task<CaseDTO?> GetByIdAsync(int caseId);
    Task<CaseDTO> CreateAsync(CreateCaseDTO dto);
    Task<CaseDTO?> UpdateAsync(int caseId, UpdateCaseDTO dto);
    Task<bool> DeleteAsync(int caseId);
}
