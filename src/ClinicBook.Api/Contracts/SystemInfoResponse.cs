namespace ClinicBook.Api.Contracts;

public sealed record SystemInfoResponse(
    string Name,
    string Version,
    string Environment,
    DateTimeOffset ServerTimeUtc);
