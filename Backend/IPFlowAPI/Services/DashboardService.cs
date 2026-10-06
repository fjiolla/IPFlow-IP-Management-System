using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Data;
using IPFlowAPI.DTOs;
using IPFlowAPI.Repositories;

namespace IPFlowAPI.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;
    private readonly IPatentRepository _patentRepository;
    private readonly ITrademarkRepository _trademarkRepository;
    private readonly ICaseRepository _caseRepository;
    private readonly IClientRepository _clientRepository;

    public DashboardService(
        ApplicationDbContext context,
        IPatentRepository patentRepository,
        ITrademarkRepository trademarkRepository,
        ICaseRepository caseRepository,
        IClientRepository clientRepository)
    {
        _context = context;
        _patentRepository = patentRepository;
        _trademarkRepository = trademarkRepository;
        _caseRepository = caseRepository;
        _clientRepository = clientRepository;
    }

    public async Task<DashboardDTO> GetDashboardDataAsync()
    {
        var totalPatents = await _patentRepository.GetTotalCountAsync();
        var activeTrademarks = await _trademarkRepository.GetTotalCountAsync("Active");
        var pendingCases = await _caseRepository.GetTotalCountAsync("Open");
        var renewalsDue = await _trademarkRepository.GetRenewalsDueCountAsync();
        var totalClients = await _clientRepository.GetTotalCountAsync();

        var upcomingDeadlines = new List<UpcomingDeadlineDTO>();
        
        var patentsWithExpiry = await _context.Patents
            .Where(p => p.ExpiryDate.HasValue && p.ExpiryDate.Value >= DateTime.UtcNow && p.ExpiryDate.Value <= DateTime.UtcNow.AddDays(30))
            .OrderBy(p => p.ExpiryDate)
            .Take(5)
            .ToListAsync();

        foreach (var patent in patentsWithExpiry)
        {
            upcomingDeadlines.Add(new UpcomingDeadlineDTO
            {
                Type = "Patent",
                Title = patent.Title,
                ReferenceNumber = patent.ApplicationNumber,
                Date = patent.ExpiryDate!.Value
            });
        }

        var trademarksWithRenewal = await _context.Trademarks
            .Where(t => t.RenewalDate.HasValue && t.RenewalDate.Value >= DateTime.UtcNow && t.RenewalDate.Value <= DateTime.UtcNow.AddDays(30))
            .OrderBy(t => t.RenewalDate)
            .Take(5)
            .ToListAsync();

        foreach (var trademark in trademarksWithRenewal)
        {
            upcomingDeadlines.Add(new UpcomingDeadlineDTO
            {
                Type = "Trademark",
                Title = trademark.Name,
                ReferenceNumber = trademark.ApplicationNumber,
                Date = trademark.RenewalDate!.Value
            });
        }

        var casesWithHearing = await _context.Cases
            .Where(c => c.NextHearingDate.HasValue && c.NextHearingDate.Value >= DateTime.UtcNow && c.NextHearingDate.Value <= DateTime.UtcNow.AddDays(30))
            .OrderBy(c => c.NextHearingDate)
            .Take(5)
            .ToListAsync();

        foreach (var caseEntity in casesWithHearing)
        {
            upcomingDeadlines.Add(new UpcomingDeadlineDTO
            {
                Type = "Case",
                Title = caseEntity.Title,
                ReferenceNumber = caseEntity.CaseNumber,
                Date = caseEntity.NextHearingDate!.Value
            });
        }

        upcomingDeadlines = upcomingDeadlines.OrderBy(d => d.Date).Take(10).ToList();

        var recentActivities = new List<RecentActivityDTO>();

        var recentPatents = await _context.Patents
            .OrderByDescending(p => p.CreatedAt)
            .Take(3)
            .ToListAsync();

        foreach (var patent in recentPatents)
        {
            recentActivities.Add(new RecentActivityDTO
            {
                Type = "Patent",
                Title = patent.Title,
                CreatedAt = patent.CreatedAt
            });
        }

        var recentTrademarks = await _context.Trademarks
            .OrderByDescending(t => t.CreatedAt)
            .Take(3)
            .ToListAsync();

        foreach (var trademark in recentTrademarks)
        {
            recentActivities.Add(new RecentActivityDTO
            {
                Type = "Trademark",
                Title = trademark.Name,
                CreatedAt = trademark.CreatedAt
            });
        }

        var recentCases = await _context.Cases
            .OrderByDescending(c => c.CreatedAt)
            .Take(3)
            .ToListAsync();

        foreach (var caseEntity in recentCases)
        {
            recentActivities.Add(new RecentActivityDTO
            {
                Type = "Case",
                Title = caseEntity.Title,
                CreatedAt = caseEntity.CreatedAt
            });
        }

        recentActivities = recentActivities.OrderByDescending(a => a.CreatedAt).Take(10).ToList();

        return new DashboardDTO
        {
            TotalPatents = totalPatents,
            ActiveTrademarks = activeTrademarks,
            PendingCases = pendingCases,
            RenewalsDue = renewalsDue,
            TotalClients = totalClients,
            UpcomingDeadlines = upcomingDeadlines,
            RecentActivities = recentActivities
        };
    }
}
