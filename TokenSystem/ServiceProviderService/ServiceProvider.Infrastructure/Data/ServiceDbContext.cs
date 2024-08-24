using Microsoft.EntityFrameworkCore;
using ServiceProvider.Domain.Entities;

namespace ServiceProvider.Infrastructure.Data;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }

    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.TokenId).IsRequired();

            entity.Property(e => e.StartedTime).IsRequired();

            entity.Property(e => e.LastUpdatedTime).IsRequired();

            entity.Property(e => e.Description).HasMaxLength(100);
        });
    }
}