using ClinicBook.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicBook.Infrastructure.Persistence.Configurations;

internal sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever(); // we generate UUIDv7 in the domain

        builder.Property(d => d.FullName)
            .HasMaxLength(Doctor.FullNameMaxLength)
            .IsRequired();

        builder.Property(d => d.Specialty)
            .HasMaxLength(Doctor.SpecialtyMaxLength)
            .IsRequired();

        builder.HasMany(d => d.Slots)
            .WithOne()
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // EF writes to the private _slots list, not the read-only property.
        builder.Navigation(d => d.Slots).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
