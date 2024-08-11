using Autofac;
using Example03.App;
using Example03.Core;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration
    .GetSection(Settings.SectionName)
    .Get<Settings>()
    .ValidateAndThrow();

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterModule(new ApplicationModule(settings));
var container = containerBuilder.Build();

var user = new User();
var notificationServices = container.Resolve<IEnumerable<INotificationService>>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
