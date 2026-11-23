using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicBook.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure: database, clock, external clients.
    /// Phase 2 adds: services.AddDbContext&lt;ClinicDb&gt;(...)
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Inject TimeProvider instead of calling DateTime.UtcNow directly,
        // so tests can swap in a fake clock. Singleton: it is stateless and thread-safe.
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
