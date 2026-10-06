namespace IPFlowAPI.Models;

public class Case
{
    public int CaseId { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public DateTime OpenDate { get; set; } = DateTime.UtcNow;
    public DateTime? CloseDate { get; set; }
    public string CaseType { get; set; } = string.Empty;
    public DateTime? NextHearingDate { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int? LawyerId { get; set; }
    public User? Lawyer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
