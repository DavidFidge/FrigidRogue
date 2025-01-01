using Castle.MicroKernel;

namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public class Mediator : IMediator
{
    // property-injected
    public IKernel Kernel { get; set; }
    
    public Mediator()
    {
    }
    
    public void Publish(INotification notification)
    {
    }

    public void Send<T>(T request) where T : IRequest
    {
        throw new NotImplementedException();
    }
}