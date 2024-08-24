using Microsoft.EntityFrameworkCore;
using TokenIssuanceService.Domain.Entities;

namespace TokenIssuanceService.Infrastructure.Data;

public class TokenDbContext : DbContext
{
    public TokenDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Token> Tokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ClientName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.IssueDateTime)
                .IsRequired();

            entity.HasIndex(e => e.ServiceCategory);

            entity.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Token>().HasData(
            new Token
            {
                Id = 1, ClientName = "John Doe", ServiceCategory = ServiceCategory.Retirees,
                Status = TokenStatus.Pending, IssueDateTime = DateTime.Now
            }
        );
    }
}