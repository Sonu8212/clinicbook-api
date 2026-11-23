using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ClinicBook.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        // Liveness: "is the process up?" Runs no checks, so it never fails
        // because of a slow dependency. Orchestrators restart the app if this fails.
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        // Readiness: "can it serve traffic?" Runs checks tagged "ready"
        // (the database check arrives in a later phase).
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("ready")
        });

        return app;
    }
}
