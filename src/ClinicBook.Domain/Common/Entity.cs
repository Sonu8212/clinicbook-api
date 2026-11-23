namespace ClinicBook.Domain.Common;

/// <summary>
/// Base type for every entity. Ids are time-ordered UUIDv7 values (.NET 9+),
/// which index far better in SQL Server than random Guid.NewGuid() values.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected init; } = Guid.CreateVersion7();
}
