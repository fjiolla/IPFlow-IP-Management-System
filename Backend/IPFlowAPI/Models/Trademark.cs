namespace IPFlowAPI.Models;

public class Trademark
{
    public int TrademarkId { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime? RenewalDate { get; set; }
    public string Status { get; set; } = "Pending";
    public string ClassNumber { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int? LawyerId { get; set; }
    public User? Lawyer { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
