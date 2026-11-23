using ClinicBook.Domain.Common;
using ClinicBook.Domain.Doctors;

namespace ClinicBook.UnitTests.Doctors;

public sealed class SlotTests
{
    private static readonly Guid DoctorId = Guid.CreateVersion7();

    [Fact]
    public void Create_throws_when_end_is_before_start()
    {
        var start = new DateTimeOffset(2026, 12, 1, 10, 0, 0, TimeSpan.Zero);
        var end = start.AddMinutes(-30);

        var ex = Assert.Throws<DomainException>(() => Slot.Create(DoctorId, start, end));

        Assert.Equal("A slot must end after it starts.", ex.Message);
    }

    [Fact]
    public void Create_stores_times_in_utc()
    {
        // 10:00 in India (UTC+05:30) is 04:30 UTC.
        var ist = TimeSpan.FromHours(5.5);
        var start = new DateTimeOffset(2026, 12, 1, 10, 0, 0, ist);
        var end = start.AddMinutes(30);

        var slot = Slot.Create(DoctorId, start, end);

        Assert.Equal(TimeSpan.Zero, slot.StartsAt.Offset);
        Assert.Equal(TimeSpan.Zero, slot.EndsAt.Offset);
        Assert.Equal(new DateTimeOffset(2026, 12, 1, 4, 30, 0, TimeSpan.Zero), slot.StartsAt);
        Assert.Equal(TimeSpan.FromMinutes(30), slot.Duration);
    }

    [Fact]
    public void Block_then_Unblock_resets_IsBlocked()
    {
        var start = new DateTimeOffset(2026, 12, 1, 10, 0, 0, TimeSpan.Zero);
        var slot = Slot.Create(DoctorId, start, start.AddMinutes(30));

        slot.Block();
        Assert.True(slot.IsBlocked);

        slot.Unblock();
        Assert.False(slot.IsBlocked);
    }
}
