using Castle.MicroKernel;

namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public class Mediator : IMediator
{
    // property-injected
    public IKernel Kernel { get; set; }
    
    public Mediator()
    {
    }
    
    public void Publish<T>(T notification) where T : INotification
    {
        var instances = Kernel.Resolve<ServiceFactory>().GetInstances<INotificationHandler<T>>();

        try
        {
            foreach (var instance in instances)
                instance.Handle(notification);
        }
        finally
        {
            foreach (var instance in instances)
                Kernel.ReleaseComponent(instance);
        }
    }

    public void Send<T>(T request) where T : IRequest
    {
        var instance = Kernel.Resolve<ServiceFactory>().GetInstance<IRequestHandler<T>>();

        try
        {
            instance.Handle(request);
        }
        finally
        {
            Kernel.ReleaseComponent(instance);
        }
    }
}
