using Example04.Core;

namespace Example04.Plugin.Teams;

public class TeamNotificationService : INotificationService
{
    public async Task NotifyAsync(User user, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        Console.WriteLine($"Teams notification sent to '{user.Id}'.");
    }
}