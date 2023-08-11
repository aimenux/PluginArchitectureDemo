using Example04.App;
using Example04.Core;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var container = new Container
{
    Options = { EnableAutoVerification = true }
};

container.RegisterApplicationPackage(configuration);

var user = new User();
var notificationServices = container.GetAllInstances<INotificationService>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
