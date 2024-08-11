using Example04.App;
using Example04.Core;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration
    .GetSection(Settings.SectionName)
    .Get<Settings>()
    .ValidateAndThrow();

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterPackage(new ApplicationPackage(settings));
var container = containerBuilder.Build();

var user = new User();
var notificationServices = container.GetAllInstances<INotificationService>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
