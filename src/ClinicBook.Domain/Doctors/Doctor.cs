using ClinicBook.Domain.Common;

namespace ClinicBook.Domain.Doctors;

/// <summary>
/// A doctor who offers appointment slots.
/// Rich domain model: private setters + factory method, so an invalid Doctor can never exist.
/// </summary>
public sealed class Doctor : Entity
{
    public const int FullNameMaxLength = 200;
    public const int SpecialtyMaxLength = 100;

    private readonly List<Slot> _slots = [];

    public string FullName { get; private set; } = null!;
    public string Specialty { get; private set; } = null!;
    public IReadOnlyCollection<Slot> Slots => _slots;

    // Required by EF Core to materialise rows. Not for application code.
    private Doctor() { }

    public static Doctor Create(string fullName, string specialty)
    {
        var doctor = new Doctor();
        doctor.Update(fullName, specialty);
        return doctor;
    }

    public void Update(string fullName, string specialty)
    {
        FullName = Guard.NotEmpty(fullName, nameof(FullName), FullNameMaxLength);
        Specialty = Guard.NotEmpty(specialty, nameof(Specialty), SpecialtyMaxLength);
    }
}
