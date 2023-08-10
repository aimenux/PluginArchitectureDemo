using Autofac;
using Example03.App;
using Example03.Core;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule(new ApplicationModule(configuration));
var container = containerBuilder.Build();

var user = new User();
var notificationServices = container.Resolve<IEnumerable<INotificationService>>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
