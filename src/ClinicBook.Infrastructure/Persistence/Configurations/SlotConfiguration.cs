using ClinicBook.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicBook.Infrastructure.Persistence.Configurations;

internal sealed class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> builder)
    {
        builder.ToTable("Slots");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Ignore(s => s.Duration); // calculated, not stored

        // Fast "free slots for doctor X on date Y" lookups,
        // and the database itself refuses two slots with the same start for one doctor.
        builder.HasIndex(s => new { s.DoctorId, s.StartsAt }).IsUnique();
    }
}
