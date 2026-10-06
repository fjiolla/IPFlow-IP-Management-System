using IPFlowAPI.DTOs;

namespace IPFlowAPI.Services;

public interface IPatentService
{
    Task<(IEnumerable<PatentDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize);
    Task<PatentDTO?> GetByIdAsync(int patentId);
    Task<PatentDTO> CreateAsync(CreatePatentDTO dto);
    Task<PatentDTO?> UpdateAsync(int patentId, UpdatePatentDTO dto);
    Task<bool> DeleteAsync(int patentId);
}
