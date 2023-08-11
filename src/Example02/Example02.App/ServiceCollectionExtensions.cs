using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example02.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetSettings(configuration);
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

    private static Settings GetSettings(IConfiguration configuration)
    {
        var path = configuration.GetValue<string>("Settings:PluginsPath");
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            throw new ArgumentException($"Path '{path}' is not valid.");
        }
        
        var pattern = configuration.GetValue<string>("Settings:PluginsPattern");
        if (string.IsNullOrWhiteSpace(pattern))
        {
            throw new ArgumentException($"Pattern '{pattern}' is not valid.");
        }

        return new Settings
        {
            PluginsPath = path,
            PluginsPattern = pattern
        };
    }
}