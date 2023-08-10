using Example02.Core;

namespace Example02.Plugin.Slack;

public class SlackNotificationService : INotificationService
{
    public async Task NotifyAsync(User user, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        Console.WriteLine($"Slack notification sent to '{user.Id}'.");
    }
}