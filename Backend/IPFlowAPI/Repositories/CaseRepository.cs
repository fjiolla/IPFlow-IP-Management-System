using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Data;
using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly ApplicationDbContext _context;

    public CaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Case>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Cases
            .Include(c => c.Client)
            .Include(c => c.Lawyer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(c => c.Status == status);
        }

        if (clientId.HasValue)
        {
            query = query.Where(c => c.ClientId == clientId.Value);
        }

        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Case?> GetByIdAsync(int caseId)
    {
        return await _context.Cases
            .Include(c => c.Client)
            .Include(c => c.Lawyer)
            .Include(c => c.Documents)
            .FirstOrDefaultAsync(c => c.CaseId == caseId);
    }

    public async Task<Case> CreateAsync(Case caseEntity)
    {
        _context.Cases.Add(caseEntity);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(caseEntity.CaseId) ?? caseEntity;
    }

    public async Task<Case?> UpdateAsync(int caseId, Case caseEntity)
    {
        var existingCase = await _context.Cases.FindAsync(caseId);
        if (existingCase == null) return null;

        existingCase.Title = caseEntity.Title;
        existingCase.Description = caseEntity.Description;
        existingCase.Status = caseEntity.Status;
        existingCase.CloseDate = caseEntity.CloseDate;
        existingCase.CaseType = caseEntity.CaseType;
        existingCase.NextHearingDate = caseEntity.NextHearingDate;
        existingCase.LawyerId = caseEntity.LawyerId;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(caseId);
    }

    public async Task<bool> DeleteAsync(int caseId)
    {
        var caseEntity = await _context.Cases.FindAsync(caseId);
        if (caseEntity == null) return false;

        _context.Cases.Remove(caseEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync(string? status = null)
    {
        var query = _context.Cases.AsQueryable();
        
        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(c => c.Status == status);
        }

        return await query.CountAsync();
    }
}
