namespace ClinicBook.Domain.Common;

/// <summary>
/// Thrown when a business rule is broken (e.g. a slot that ends before it starts).
/// The API turns this into a 400/422 ProblemDetails response.
/// </summary>
public sealed class DomainException(string message) : Exception(message);
