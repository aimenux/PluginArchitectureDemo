using System.Reflection;
using Example04.Core;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Example04.App;

public class ApplicationPackage : IPackage
{
    private readonly Settings _settings;

    public ApplicationPackage(Settings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public void RegisterServices(Container container)
    {
        var assemblies = Directory.EnumerateFiles(_settings.PluginsPath, _settings.PluginsPattern)
            .Select(Assembly.LoadFrom)
            .ToList();
        
        container.Collection.Register<INotificationService>(assemblies);
    }
}