namespace IPFlowAPI.DTOs;

public class TrademarkDTO
{
    public int TrademarkId { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime? RenewalDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ClassNumber { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int? LawyerId { get; set; }
    public string? LawyerName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateTrademarkDTO
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public DateTime? RenewalDate { get; set; }
    public string Status { get; set; } = "Pending";
    public string ClassNumber { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int? LawyerId { get; set; }
}

public class UpdateTrademarkDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? RenewalDate { get; set; }
    public string? Status { get; set; }
    public string? ClassNumber { get; set; }
    public int? LawyerId { get; set; }
}
