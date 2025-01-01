namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public interface IRequestHandler<T> where T : IRequest
{
    void Handle(T request);
}