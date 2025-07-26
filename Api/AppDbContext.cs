namespace Api;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using NodaTime;
using Api.Calculations;

public class AppDbContext : DbContext
{
    public DbSet<Calculation> Calculations { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculation>()
            .Property(e => e.Operands)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<double>>(v, (JsonSerializerOptions?)null) ?? new List<double>(),
                new ValueComparer<List<double>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );

        modelBuilder.Entity<Calculation>()
            .Property(e => e.CreatedAt)
            .HasConversion(
                v => v.ToDateTimeUtc(),
                v => Instant.FromDateTimeUtc(DateTime.SpecifyKind(v, DateTimeKind.Utc))
            );
    }
}
