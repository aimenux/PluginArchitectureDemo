using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Example02.App;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, Settings settings)
    {
        var assemblies = Directory.EnumerateFiles(settings.PluginsPath, settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .ToList();
        
        foreach (var assembly in assemblies)
        {
            services.Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        }
        
        return services;
    }
}