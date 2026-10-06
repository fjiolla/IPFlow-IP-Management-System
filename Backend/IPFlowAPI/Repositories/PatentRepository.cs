using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Data;
using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public class PatentRepository : IPatentRepository
{
    private readonly ApplicationDbContext _context;

    public PatentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patent>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Patents
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(p => p.Status == status);
        }

        if (clientId.HasValue)
        {
            query = query.Where(p => p.ClientId == clientId.Value);
        }

        return await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Patent?> GetByIdAsync(int patentId)
    {
        return await _context.Patents
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .Include(p => p.Documents)
            .FirstOrDefaultAsync(p => p.PatentId == patentId);
    }

    public async Task<Patent> CreateAsync(Patent patent)
    {
        _context.Patents.Add(patent);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(patent.PatentId) ?? patent;
    }

    public async Task<Patent?> UpdateAsync(int patentId, Patent patent)
    {
        var existingPatent = await _context.Patents.FindAsync(patentId);
        if (existingPatent == null) return null;

        existingPatent.Title = patent.Title;
        existingPatent.Description = patent.Description;
        existingPatent.FilingDate = patent.FilingDate;
        existingPatent.ExpiryDate = patent.ExpiryDate;
        existingPatent.Status = patent.Status;
        existingPatent.LawyerId = patent.LawyerId;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(patentId);
    }

    public async Task<bool> DeleteAsync(int patentId)
    {
        var patent = await _context.Patents.FindAsync(patentId);
        if (patent == null) return false;

        _context.Patents.Remove(patent);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync(string? status = null)
    {
        var query = _context.Patents.AsQueryable();
        
        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(p => p.Status == status);
        }

        return await query.CountAsync();
    }
}
