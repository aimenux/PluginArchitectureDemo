using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Example04.App;

public static class DependencyInjectionExtensions
{
    public static void RegisterApplicationPackage(this Container container, IConfiguration configuration)
    {
        var package = new ApplicationPackage(configuration);
        package.RegisterServices(container);
    }
}