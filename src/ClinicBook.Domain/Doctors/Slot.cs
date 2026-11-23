using ClinicBook.Domain.Common;

namespace ClinicBook.Domain.Doctors;

/// <summary>
/// A bookable block of a doctor's time. Times are always stored in UTC.
/// </summary>
public sealed class Slot : Entity
{
    public Guid DoctorId { get; private set; }
    public DateTimeOffset StartsAt { get; private set; }
    public DateTimeOffset EndsAt { get; private set; }
    public bool IsBlocked { get; private set; }

    public TimeSpan Duration => EndsAt - StartsAt;

    // Required by EF Core.
    private Slot() { }

    public static Slot Create(Guid doctorId, DateTimeOffset startsAt, DateTimeOffset endsAt)
    {
        if (doctorId == Guid.Empty)
        {
            throw new DomainException("Doctor id is required.");
        }

        if (endsAt <= startsAt)
        {
            throw new DomainException("A slot must end after it starts.");
        }

        return new Slot
        {
            DoctorId = doctorId,
            StartsAt = startsAt.ToUniversalTime(),
            EndsAt = endsAt.ToUniversalTime()
        };
    }

    public void Block() => IsBlocked = true;

    public void Unblock() => IsBlocked = false;
}
