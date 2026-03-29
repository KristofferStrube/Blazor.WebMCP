using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// Extensions for the <see cref="IServiceCollection"/> from the Blazor.WebMCP library.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds <see cref="IModelContextService"/> as a scoped service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddModelContextService(this IServiceCollection services)
    {
        return services.AddScoped<IModelContextService, ModelContextService>();
    }
}
