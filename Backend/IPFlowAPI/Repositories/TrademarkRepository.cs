using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Data;
using IPFlowAPI.Models;

namespace IPFlowAPI.Repositories;

public class TrademarkRepository : ITrademarkRepository
{
    private readonly ApplicationDbContext _context;

    public TrademarkRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trademark>> GetAllAsync(string? status = null, int? clientId = null, int pageNumber = 1, int pageSize = 10)
    {
        var query = _context.Trademarks
            .Include(t => t.Client)
            .Include(t => t.Lawyer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(t => t.Status == status);
        }

        if (clientId.HasValue)
        {
            query = query.Where(t => t.ClientId == clientId.Value);
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Trademark?> GetByIdAsync(int trademarkId)
    {
        return await _context.Trademarks
            .Include(t => t.Client)
            .Include(t => t.Lawyer)
            .Include(t => t.Documents)
            .FirstOrDefaultAsync(t => t.TrademarkId == trademarkId);
    }

    public async Task<Trademark> CreateAsync(Trademark trademark)
    {
        _context.Trademarks.Add(trademark);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(trademark.TrademarkId) ?? trademark;
    }

    public async Task<Trademark?> UpdateAsync(int trademarkId, Trademark trademark)
    {
        var existingTrademark = await _context.Trademarks.FindAsync(trademarkId);
        if (existingTrademark == null) return null;

        existingTrademark.Name = trademark.Name;
        existingTrademark.Description = trademark.Description;
        existingTrademark.RegistrationDate = trademark.RegistrationDate;
        existingTrademark.RenewalDate = trademark.RenewalDate;
        existingTrademark.Status = trademark.Status;
        existingTrademark.ClassNumber = trademark.ClassNumber;
        existingTrademark.LawyerId = trademark.LawyerId;

        await _context.SaveChangesAsync();
        return await GetByIdAsync(trademarkId);
    }

    public async Task<bool> DeleteAsync(int trademarkId)
    {
        var trademark = await _context.Trademarks.FindAsync(trademarkId);
        if (trademark == null) return false;

        _context.Trademarks.Remove(trademark);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync(string? status = null)
    {
        var query = _context.Trademarks.AsQueryable();
        
        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(t => t.Status == status);
        }

        return await query.CountAsync();
    }

    public async Task<int> GetRenewalsDueCountAsync()
    {
        var thirtyDaysFromNow = DateTime.UtcNow.AddDays(30);
        return await _context.Trademarks
            .Where(t => t.RenewalDate.HasValue && t.RenewalDate.Value <= thirtyDaysFromNow)
            .CountAsync();
    }
}
