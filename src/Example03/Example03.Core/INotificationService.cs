namespace Example03.Core;

public interface INotificationService
{
    Task NotifyAsync(User user, CancellationToken cancellationToken = default);
}