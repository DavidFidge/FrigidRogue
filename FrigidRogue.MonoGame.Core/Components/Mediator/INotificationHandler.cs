namespace FrigidRogue.MonoGame.Core.Components.Mediator;


public interface INotificationHandler<T> where T : INotification
{
    void Handle(T request);
}