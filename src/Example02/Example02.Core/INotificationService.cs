namespace Example02.Core;

public interface INotificationService
{
    Task NotifyAsync(User user, CancellationToken cancellationToken = default);
}