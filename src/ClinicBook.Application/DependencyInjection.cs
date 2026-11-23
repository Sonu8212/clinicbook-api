using Microsoft.Extensions.DependencyInjection;

namespace ClinicBook.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers application services (business logic).
    /// Phase 2 adds: services.AddScoped&lt;IDoctorService, DoctorService&gt;();
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
