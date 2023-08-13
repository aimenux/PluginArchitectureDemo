using System.Reflection;
using Autofac;

namespace Example03.App;

public class ApplicationModule : Autofac.Module
{
    private readonly Settings _settings;

    public ApplicationModule(Settings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = Directory.EnumerateFiles(_settings.PluginsPath, _settings.PluginsPattern)
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
}