using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Example01.App;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, Settings settings)
    {
        var implementationTypes = Directory.EnumerateFiles(settings.PluginsPath, settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass)
            .ToList();

        foreach (var implementationType in implementationTypes)
        {
            foreach(var interfaceType in implementationType.GetInterfaces())
            {
                services.AddSingleton(interfaceType, implementationType);
            }
        }
        
        return services;
    }
}