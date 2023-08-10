using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Example03.App;

public class ApplicationModule : Autofac.Module
{
    private readonly IConfiguration _configuration;

    public ApplicationModule(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        var settings = GetSettings();
        var assemblies = Directory.EnumerateFiles(settings.PluginsPath, settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .ToList();
        foreach (var assembly in assemblies)
        {
            builder
                .RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
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