namespace FrigidRogue.MonoGame.Core.Components.Mediator;

public class Mediator : IMediator
{
    private readonly ServiceFactory _serviceFactory;

    public Mediator(ServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
    
    public void Publish<T>(T notification) where T : INotification
    {
        var instances = _serviceFactory.GetInstances<INotificationHandler<T>>();

        foreach (var instance in instances)
        {
            instance.Handle(notification);
        }
    }

    public void Send<T>(T request) where T : IRequest
    {
        var instance = _serviceFactory.GetInstance<IRequestHandler<T>>();
        instance.Handle(request);
    }
}
