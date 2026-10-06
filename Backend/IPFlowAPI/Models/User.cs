namespace IPFlowAPI.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Patent> PatentsAsLawyer { get; set; } = new List<Patent>();
    public ICollection<Trademark> TrademarksAsLawyer { get; set; } = new List<Trademark>();
    public ICollection<Case> CasesAsLawyer { get; set; } = new List<Case>();
}
