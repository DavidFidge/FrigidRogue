namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public interface IMediator
{
    void Publish<T>(T notification) where T : INotification;
    void Send<T>(T request) where T : IRequest;
}