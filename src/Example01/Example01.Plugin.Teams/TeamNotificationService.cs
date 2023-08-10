using Example01.Core;

namespace Example01.Plugin.Teams;

public class TeamNotificationService : INotificationService
{
    public async Task NotifyAsync(User user, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        Console.WriteLine($"Teams notification sent to '{user.Id}'.");
    }
}