using IPFlowAPI.DTOs;
using IPFlowAPI.Models;
using IPFlowAPI.Repositories;

namespace IPFlowAPI.Services;

public class CaseService : ICaseService
{
    private readonly ICaseRepository _caseRepository;

    public CaseService(ICaseRepository caseRepository)
    {
        _caseRepository = caseRepository;
    }

    public async Task<(IEnumerable<CaseDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize)
    {
        var cases = await _caseRepository.GetAllAsync(status, clientId, pageNumber, pageSize);
        var totalCount = await _caseRepository.GetTotalCountAsync(status);

        var caseDTOs = cases.Select(c => new CaseDTO
        {
            CaseId = c.CaseId,
            CaseNumber = c.CaseNumber,
            Title = c.Title,
            Description = c.Description,
            Status = c.Status,
            OpenDate = c.OpenDate,
            CloseDate = c.CloseDate,
            CaseType = c.CaseType,
            NextHearingDate = c.NextHearingDate,
            ClientId = c.ClientId,
            ClientName = c.Client?.Name ?? string.Empty,
            LawyerId = c.LawyerId,
            LawyerName = c.Lawyer?.Name,
            CreatedAt = c.CreatedAt
        });

        return (caseDTOs, totalCount);
    }

    public async Task<CaseDTO?> GetByIdAsync(int caseId)
    {
        var caseEntity = await _caseRepository.GetByIdAsync(caseId);
        if (caseEntity == null) return null;

        return new CaseDTO
        {
            CaseId = caseEntity.CaseId,
            CaseNumber = caseEntity.CaseNumber,
            Title = caseEntity.Title,
            Description = caseEntity.Description,
            Status = caseEntity.Status,
            OpenDate = caseEntity.OpenDate,
            CloseDate = caseEntity.CloseDate,
            CaseType = caseEntity.CaseType,
            NextHearingDate = caseEntity.NextHearingDate,
            ClientId = caseEntity.ClientId,
            ClientName = caseEntity.Client?.Name ?? string.Empty,
            LawyerId = caseEntity.LawyerId,
            LawyerName = caseEntity.Lawyer?.Name,
            CreatedAt = caseEntity.CreatedAt
        };
    }

    public async Task<CaseDTO> CreateAsync(CreateCaseDTO dto)
    {
        var caseEntity = new Case
        {
            CaseNumber = dto.CaseNumber,
            Title = dto.Title,
            Description = dto.Description,
            Status = dto.Status,
            OpenDate = dto.OpenDate,
            CaseType = dto.CaseType,
            NextHearingDate = dto.NextHearingDate,
            ClientId = dto.ClientId,
            LawyerId = dto.LawyerId
        };

        var createdCase = await _caseRepository.CreateAsync(caseEntity);

        return new CaseDTO
        {
            CaseId = createdCase.CaseId,
            CaseNumber = createdCase.CaseNumber,
            Title = createdCase.Title,
            Description = createdCase.Description,
            Status = createdCase.Status,
            OpenDate = createdCase.OpenDate,
            CloseDate = createdCase.CloseDate,
            CaseType = createdCase.CaseType,
            NextHearingDate = createdCase.NextHearingDate,
            ClientId = createdCase.ClientId,
            ClientName = createdCase.Client?.Name ?? string.Empty,
            LawyerId = createdCase.LawyerId,
            LawyerName = createdCase.Lawyer?.Name,
            CreatedAt = createdCase.CreatedAt
        };
    }

    public async Task<CaseDTO?> UpdateAsync(int caseId, UpdateCaseDTO dto)
    {
        var existingCase = await _caseRepository.GetByIdAsync(caseId);
        if (existingCase == null) return null;

        if (!string.IsNullOrEmpty(dto.Title))
            existingCase.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.Description))
            existingCase.Description = dto.Description;
        if (!string.IsNullOrEmpty(dto.Status))
            existingCase.Status = dto.Status;
        if (dto.CloseDate.HasValue)
            existingCase.CloseDate = dto.CloseDate;
        if (!string.IsNullOrEmpty(dto.CaseType))
            existingCase.CaseType = dto.CaseType;
        if (dto.NextHearingDate.HasValue)
            existingCase.NextHearingDate = dto.NextHearingDate;
        if (dto.LawyerId.HasValue)
            existingCase.LawyerId = dto.LawyerId;

        var updatedCase = await _caseRepository.UpdateAsync(caseId, existingCase);
        if (updatedCase == null) return null;

        return new CaseDTO
        {
            CaseId = updatedCase.CaseId,
            CaseNumber = updatedCase.CaseNumber,
            Title = updatedCase.Title,
            Description = updatedCase.Description,
            Status = updatedCase.Status,
            OpenDate = updatedCase.OpenDate,
            CloseDate = updatedCase.CloseDate,
            CaseType = updatedCase.CaseType,
            NextHearingDate = updatedCase.NextHearingDate,
            ClientId = updatedCase.ClientId,
            ClientName = updatedCase.Client?.Name ?? string.Empty,
            LawyerId = updatedCase.LawyerId,
            LawyerName = updatedCase.Lawyer?.Name,
            CreatedAt = updatedCase.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int caseId)
    {
        return await _caseRepository.DeleteAsync(caseId);
    }
}
