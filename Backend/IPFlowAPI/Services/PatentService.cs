using IPFlowAPI.DTOs;
using IPFlowAPI.Models;
using IPFlowAPI.Repositories;

namespace IPFlowAPI.Services;

public class PatentService : IPatentService
{
    private readonly IPatentRepository _patentRepository;

    public PatentService(IPatentRepository patentRepository)
    {
        _patentRepository = patentRepository;
    }

    public async Task<(IEnumerable<PatentDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize)
    {
        var patents = await _patentRepository.GetAllAsync(status, clientId, pageNumber, pageSize);
        var totalCount = await _patentRepository.GetTotalCountAsync(status);

        var patentDTOs = patents.Select(p => new PatentDTO
        {
            PatentId = p.PatentId,
            ApplicationNumber = p.ApplicationNumber,
            Title = p.Title,
            Description = p.Description,
            FilingDate = p.FilingDate,
            ExpiryDate = p.ExpiryDate,
            Status = p.Status,
            ClientId = p.ClientId,
            ClientName = p.Client?.Name ?? string.Empty,
            LawyerId = p.LawyerId,
            LawyerName = p.Lawyer?.Name,
            CreatedAt = p.CreatedAt
        });

        return (patentDTOs, totalCount);
    }

    public async Task<PatentDTO?> GetByIdAsync(int patentId)
    {
        var patent = await _patentRepository.GetByIdAsync(patentId);
        if (patent == null) return null;

        return new PatentDTO
        {
            PatentId = patent.PatentId,
            ApplicationNumber = patent.ApplicationNumber,
            Title = patent.Title,
            Description = patent.Description,
            FilingDate = patent.FilingDate,
            ExpiryDate = patent.ExpiryDate,
            Status = patent.Status,
            ClientId = patent.ClientId,
            ClientName = patent.Client?.Name ?? string.Empty,
            LawyerId = patent.LawyerId,
            LawyerName = patent.Lawyer?.Name,
            CreatedAt = patent.CreatedAt
        };
    }

    public async Task<PatentDTO> CreateAsync(CreatePatentDTO dto)
    {
        var patent = new Patent
        {
            ApplicationNumber = dto.ApplicationNumber,
            Title = dto.Title,
            Description = dto.Description,
            FilingDate = dto.FilingDate,
            ExpiryDate = dto.ExpiryDate,
            Status = dto.Status,
            ClientId = dto.ClientId,
            LawyerId = dto.LawyerId
        };

        var createdPatent = await _patentRepository.CreateAsync(patent);

        return new PatentDTO
        {
            PatentId = createdPatent.PatentId,
            ApplicationNumber = createdPatent.ApplicationNumber,
            Title = createdPatent.Title,
            Description = createdPatent.Description,
            FilingDate = createdPatent.FilingDate,
            ExpiryDate = createdPatent.ExpiryDate,
            Status = createdPatent.Status,
            ClientId = createdPatent.ClientId,
            ClientName = createdPatent.Client?.Name ?? string.Empty,
            LawyerId = createdPatent.LawyerId,
            LawyerName = createdPatent.Lawyer?.Name,
            CreatedAt = createdPatent.CreatedAt
        };
    }

    public async Task<PatentDTO?> UpdateAsync(int patentId, UpdatePatentDTO dto)
    {
        var existingPatent = await _patentRepository.GetByIdAsync(patentId);
        if (existingPatent == null) return null;

        if (!string.IsNullOrEmpty(dto.Title))
            existingPatent.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.Description))
            existingPatent.Description = dto.Description;
        if (dto.FilingDate.HasValue)
            existingPatent.FilingDate = dto.FilingDate.Value;
        if (dto.ExpiryDate.HasValue)
            existingPatent.ExpiryDate = dto.ExpiryDate;
        if (!string.IsNullOrEmpty(dto.Status))
            existingPatent.Status = dto.Status;
        if (dto.LawyerId.HasValue)
            existingPatent.LawyerId = dto.LawyerId;

        var updatedPatent = await _patentRepository.UpdateAsync(patentId, existingPatent);
        if (updatedPatent == null) return null;

        return new PatentDTO
        {
            PatentId = updatedPatent.PatentId,
            ApplicationNumber = updatedPatent.ApplicationNumber,
            Title = updatedPatent.Title,
            Description = updatedPatent.Description,
            FilingDate = updatedPatent.FilingDate,
            ExpiryDate = updatedPatent.ExpiryDate,
            Status = updatedPatent.Status,
            ClientId = updatedPatent.ClientId,
            ClientName = updatedPatent.Client?.Name ?? string.Empty,
            LawyerId = updatedPatent.LawyerId,
            LawyerName = updatedPatent.Lawyer?.Name,
            CreatedAt = updatedPatent.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int patentId)
    {
        return await _patentRepository.DeleteAsync(patentId);
    }
}
