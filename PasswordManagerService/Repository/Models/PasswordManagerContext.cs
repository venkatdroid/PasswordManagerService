
using Microsoft.EntityFrameworkCore;

namespace PasswordManagerService.Repository.Models;

public partial class PasswordManagerContext : DbContext
{
    public PasswordManagerContext()
    {
    }

    public PasswordManagerContext(DbContextOptions<PasswordManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Password> Passwords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         //optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PasswordManager;User Id=SA;Password=Admin@123; TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PASSWORD");

            entity.ToTable("Password");

            entity.Property(e => e.App).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.EncryptedPassword)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Username).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
