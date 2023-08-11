﻿using Example02.App;
using Example02.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddServices(configuration)
    .BuildServiceProvider();

var user = new User();
var notificationServices = serviceProvider.GetServices<INotificationService>();
foreach (var notificationService in notificationServices)
{
    await notificationService.NotifyAsync(user);
}
