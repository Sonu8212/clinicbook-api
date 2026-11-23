using ClinicBook.Domain.Doctors;
using Microsoft.EntityFrameworkCore;

namespace ClinicBook.Infrastructure.Persistence;

/// <summary>
/// EF Core unit of work for the clinic. Registered as Scoped: one instance per HTTP request.
/// DbContext is NOT thread-safe, so never share one across parallel tasks.
/// </summary>
public sealed class ClinicDb(DbContextOptions<ClinicDb> options) : DbContext(options)
{
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Slot> Slots => Set<Slot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Picks up every IEntityTypeConfiguration<T> in this assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDb).Assembly);
    }
}
