using ClinicBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicBook.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure: database, clock, external clients.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Inject TimeProvider instead of calling DateTime.UtcNow directly,
        // so tests can swap in a fake clock. Singleton: it is stateless and thread-safe.
        services.AddSingleton(TimeProvider.System);

        // AddDbContext registers ClinicDb as Scoped (one per request).
        // The connection string comes from User Secrets locally, environment variables in production.
        services.AddDbContext<ClinicDb>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ClinicDb")));

        return services;
    }
}
