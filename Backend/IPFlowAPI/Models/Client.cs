namespace IPFlowAPI.Models;

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Patent> Patents { get; set; } = new List<Patent>();
    public ICollection<Trademark> Trademarks { get; set; } = new List<Trademark>();
    public ICollection<Case> Cases { get; set; } = new List<Case>();
}
