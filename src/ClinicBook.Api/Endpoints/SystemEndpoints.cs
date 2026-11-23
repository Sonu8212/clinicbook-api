using ClinicBook.Api.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClinicBook.Api.Endpoints;

public static class SystemEndpoints
{
    public static IEndpointRouteBuilder MapSystemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/system")
            .WithTags("System");

        group.MapGet("/info", GetInfo)
            .WithName("GetSystemInfo")
            .WithSummary("Returns API name, version, environment and server time.");

        return app;
    }

    // TimeProvider and IHostEnvironment are injected by DI straight into the handler.
    // The Ok<T> return type tells OpenAPI exactly what this endpoint returns.
    private static Ok<SystemInfoResponse> GetInfo(TimeProvider clock, IHostEnvironment env) =>
        TypedResults.Ok(new SystemInfoResponse(
            Name: "ClinicBook API",
            Version: "v1",
            Environment: env.EnvironmentName,
            ServerTimeUtc: clock.GetUtcNow()));
}
