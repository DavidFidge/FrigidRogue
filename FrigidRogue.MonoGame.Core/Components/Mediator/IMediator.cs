namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public interface IMediator
{
    void Publish(INotification notification);
    void Send<T>(T request) where T : IRequest;
}