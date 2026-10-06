using Microsoft.EntityFrameworkCore;
using IPFlowAPI.Models;

namespace IPFlowAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Patent> Patents { get; set; }
    public DbSet<Trademark> Trademarks { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleName).IsRequired().HasMaxLength(50);
            entity.HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Lawyer" },
                new Role { RoleId = 3, RoleName = "Client" }
            );
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Patent>(entity =>
        {
            entity.HasKey(e => e.PatentId);
            entity.HasIndex(e => e.ApplicationNumber).IsUnique();
            entity.Property(e => e.ApplicationNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.HasOne(e => e.Client)
                .WithMany(c => c.Patents)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Lawyer)
                .WithMany(u => u.PatentsAsLawyer)
                .HasForeignKey(e => e.LawyerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Trademark>(entity =>
        {
            entity.HasKey(e => e.TrademarkId);
            entity.HasIndex(e => e.ApplicationNumber).IsUnique();
            entity.Property(e => e.ApplicationNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ClassNumber).HasMaxLength(20);
            entity.HasOne(e => e.Client)
                .WithMany(c => c.Trademarks)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Lawyer)
                .WithMany(u => u.TrademarksAsLawyer)
                .HasForeignKey(e => e.LawyerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId);
            entity.HasIndex(e => e.CaseNumber).IsUnique();
            entity.Property(e => e.CaseNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.CaseType).HasMaxLength(50);
            entity.HasOne(e => e.Client)
                .WithMany(c => c.Cases)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Lawyer)
                .WithMany(u => u.CasesAsLawyer)
                .HasForeignKey(e => e.LawyerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.HasOne(e => e.Patent)
                .WithMany(p => p.Documents)
                .HasForeignKey(e => e.PatentId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Trademark)
                .WithMany(t => t.Documents)
                .HasForeignKey(e => e.TrademarkId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Case)
                .WithMany(c => c.Documents)
                .HasForeignKey(e => e.CaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
