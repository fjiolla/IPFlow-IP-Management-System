namespace IPFlowAPI.DTOs;

public class CaseDTO
{
    public int CaseId { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public string CaseType { get; set; } = string.Empty;
    public DateTime? NextHearingDate { get; set; }
    public int ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public int? LawyerId { get; set; }
    public string? LawyerName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCaseDTO
{
    public string CaseNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Open";
    public DateTime OpenDate { get; set; } = DateTime.UtcNow;
    public string CaseType { get; set; } = string.Empty;
    public DateTime? NextHearingDate { get; set; }
    public int ClientId { get; set; }
    public int? LawyerId { get; set; }
}

public class UpdateCaseDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public DateTime? CloseDate { get; set; }
    public string? CaseType { get; set; }
    public DateTime? NextHearingDate { get; set; }
    public int? LawyerId { get; set; }
}
