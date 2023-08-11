using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example01.App;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetSettings(configuration);
        var assemblies = Directory.EnumerateFiles(settings.PluginsPath, settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .ToList();
        foreach (var implementationType in assemblies.SelectMany(x => x.GetTypes()).Where(x => x.IsClass))
        {
            foreach(var interfaceType in implementationType.GetInterfaces())
            {
                services.AddSingleton(interfaceType, implementationType);
            }
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