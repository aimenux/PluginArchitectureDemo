using SimpleInjector;
using SimpleInjector.Packaging;

namespace Example04.App;

public class ContainerBuilder
{
    private readonly Container _container = new Container
    {
        Options = { EnableAutoVerification = true }
    };

    public ContainerBuilder RegisterPackage(IPackage package)
    {
        package.RegisterServices(_container);
        return this;
    }
    
    public Container Build()
    {
        return _container;
    }
}