using Example01.App;
using Example01.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var settings = configuration
    .GetSection(Settings.SectionName)
    .Get<Settings>()
    .ValidateAndThrow();

var serviceProvider = new ServiceCollection()
    .AddServices(settings)
    .BuildServiceProvider();

var user = new User();
var notificationServices = serviceProvider.GetServices<INotificationService>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
