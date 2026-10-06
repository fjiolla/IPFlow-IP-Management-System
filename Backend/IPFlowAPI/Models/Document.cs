namespace IPFlowAPI.Models;

public class Document
{
    public int DocumentId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int? PatentId { get; set; }
    public Patent? Patent { get; set; }
    public int? TrademarkId { get; set; }
    public Trademark? Trademark { get; set; }
    public int? CaseId { get; set; }
    public Case? Case { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
