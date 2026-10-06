namespace IPFlowAPI.DTOs;

public class PatentDTO
{
    public int PatentId { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime FilingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int? LawyerId { get; set; }
    public string? LawyerName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreatePatentDTO
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime FilingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string Status { get; set; } = "Pending";
    public int ClientId { get; set; }
    public int? LawyerId { get; set; }
}

public class UpdatePatentDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? FilingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Status { get; set; }
    public int? LawyerId { get; set; }
}
