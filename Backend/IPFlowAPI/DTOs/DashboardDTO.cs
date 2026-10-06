namespace IPFlowAPI.DTOs;

public class DashboardDTO
{
    public int TotalPatents { get; set; }
    public int ActiveTrademarks { get; set; }
    public int PendingCases { get; set; }
    public int RenewalsDue { get; set; }
    public int TotalClients { get; set; }
    public List<UpcomingDeadlineDTO> UpcomingDeadlines { get; set; } = new();
    public List<RecentActivityDTO> RecentActivities { get; set; } = new();
}

public class UpcomingDeadlineDTO
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}

public class RecentActivityDTO
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
