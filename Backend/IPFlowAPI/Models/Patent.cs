namespace IPFlowAPI.Models;

public class Patent
{
    public int PatentId { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime FilingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Status { get; set; } = "Pending";
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int? LawyerId { get; set; }
    public User? Lawyer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
