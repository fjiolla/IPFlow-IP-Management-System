using IPFlowAPI.DTOs;
using IPFlowAPI.Models;
using IPFlowAPI.Repositories;

namespace IPFlowAPI.Services;

public class TrademarkService : ITrademarkService
{
    private readonly ITrademarkRepository _trademarkRepository;

    public TrademarkService(ITrademarkRepository trademarkRepository)
    {
        _trademarkRepository = trademarkRepository;
    }

    public async Task<(IEnumerable<TrademarkDTO>, int)> GetAllAsync(string? status, int? clientId, int pageNumber, int pageSize)
    {
        var trademarks = await _trademarkRepository.GetAllAsync(status, clientId, pageNumber, pageSize);
        var totalCount = await _trademarkRepository.GetTotalCountAsync(status);

        var trademarkDTOs = trademarks.Select(t => new TrademarkDTO
        {
            TrademarkId = t.TrademarkId,
            ApplicationNumber = t.ApplicationNumber,
            Name = t.Name,
            Description = t.Description,
            RegistrationDate = t.RegistrationDate,
            RenewalDate = t.RenewalDate,
            Status = t.Status,
            ClassNumber = t.ClassNumber,
            ClientId = t.ClientId,
            ClientName = t.Client?.Name ?? string.Empty,
            LawyerId = t.LawyerId,
            LawyerName = t.Lawyer?.Name,
            CreatedAt = t.CreatedAt
        });

        return (trademarkDTOs, totalCount);
    }

    public async Task<TrademarkDTO?> GetByIdAsync(int trademarkId)
    {
        var trademark = await _trademarkRepository.GetByIdAsync(trademarkId);
        if (trademark == null) return null;

        return new TrademarkDTO
        {
            TrademarkId = trademark.TrademarkId,
            ApplicationNumber = trademark.ApplicationNumber,
            Name = trademark.Name,
            Description = trademark.Description,
            RegistrationDate = trademark.RegistrationDate,
            RenewalDate = trademark.RenewalDate,
            Status = trademark.Status,
            ClassNumber = trademark.ClassNumber,
            ClientId = trademark.ClientId,
            ClientName = trademark.Client?.Name ?? string.Empty,
            LawyerId = trademark.LawyerId,
            LawyerName = trademark.Lawyer?.Name,
            CreatedAt = trademark.CreatedAt
        };
    }

    public async Task<TrademarkDTO> CreateAsync(CreateTrademarkDTO dto)
    {
        var trademark = new Trademark
        {
            ApplicationNumber = dto.ApplicationNumber,
            Name = dto.Name,
            Description = dto.Description,
            RegistrationDate = dto.RegistrationDate,
            RenewalDate = dto.RenewalDate,
            Status = dto.Status,
            ClassNumber = dto.ClassNumber,
            ClientId = dto.ClientId,
            LawyerId = dto.LawyerId
        };

        var createdTrademark = await _trademarkRepository.CreateAsync(trademark);

        return new TrademarkDTO
        {
            TrademarkId = createdTrademark.TrademarkId,
            ApplicationNumber = createdTrademark.ApplicationNumber,
            Name = createdTrademark.Name,
            Description = createdTrademark.Description,
            RegistrationDate = createdTrademark.RegistrationDate,
            RenewalDate = createdTrademark.RenewalDate,
            Status = createdTrademark.Status,
            ClassNumber = createdTrademark.ClassNumber,
            ClientId = createdTrademark.ClientId,
            ClientName = createdTrademark.Client?.Name ?? string.Empty,
            LawyerId = createdTrademark.LawyerId,
            LawyerName = createdTrademark.Lawyer?.Name,
            CreatedAt = createdTrademark.CreatedAt
        };
    }

    public async Task<TrademarkDTO?> UpdateAsync(int trademarkId, UpdateTrademarkDTO dto)
    {
        var existingTrademark = await _trademarkRepository.GetByIdAsync(trademarkId);
        if (existingTrademark == null) return null;

        if (!string.IsNullOrEmpty(dto.Name))
            existingTrademark.Name = dto.Name;
        if (!string.IsNullOrEmpty(dto.Description))
            existingTrademark.Description = dto.Description;
        if (dto.RegistrationDate.HasValue)
            existingTrademark.RegistrationDate = dto.RegistrationDate.Value;
        if (dto.RenewalDate.HasValue)
            existingTrademark.RenewalDate = dto.RenewalDate;
        if (!string.IsNullOrEmpty(dto.Status))
            existingTrademark.Status = dto.Status;
        if (!string.IsNullOrEmpty(dto.ClassNumber))
            existingTrademark.ClassNumber = dto.ClassNumber;
        if (dto.LawyerId.HasValue)
            existingTrademark.LawyerId = dto.LawyerId;

        var updatedTrademark = await _trademarkRepository.UpdateAsync(trademarkId, existingTrademark);
        if (updatedTrademark == null) return null;

        return new TrademarkDTO
        {
            TrademarkId = updatedTrademark.TrademarkId,
            ApplicationNumber = updatedTrademark.ApplicationNumber,
            Name = updatedTrademark.Name,
            Description = updatedTrademark.Description,
            RegistrationDate = updatedTrademark.RegistrationDate,
            RenewalDate = updatedTrademark.RenewalDate,
            Status = updatedTrademark.Status,
            ClassNumber = updatedTrademark.ClassNumber,
            ClientId = updatedTrademark.ClientId,
            ClientName = updatedTrademark.Client?.Name ?? string.Empty,
            LawyerId = updatedTrademark.LawyerId,
            LawyerName = updatedTrademark.Lawyer?.Name,
            CreatedAt = updatedTrademark.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(int trademarkId)
    {
        return await _trademarkRepository.DeleteAsync(trademarkId);
    }
}
