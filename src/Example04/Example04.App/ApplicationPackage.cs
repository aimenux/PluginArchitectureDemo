using System.Reflection;
using Example04.Core;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Example04.App;

public class ApplicationPackage : IPackage
{
    private readonly IConfiguration _configuration;

    public ApplicationPackage(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    public void RegisterServices(Container container)
    {
        var settings = GetSettings();
        var assemblies = Directory.EnumerateFiles(settings.PluginsPath, settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .ToList();
        container.Collection.Register<INotificationService>(assemblies);
    }
    
    private Settings GetSettings()
    {
        var path = _configuration.GetValue<string>("Settings:PluginsPath");
        if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
        {
            throw new ArgumentException($"Path '{path}' is not valid.");
        }
        
        var pattern = _configuration.GetValue<string>("Settings:PluginsPattern");
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