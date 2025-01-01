using System.Threading;
using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Messages;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public abstract class BaseViewModel<T> : BaseGameComponent, IRequestHandler<InterfaceRequest<T>>
        where T : new()
    {
        public T Data { get; protected set; }

        public virtual void Initialize()
        {
            Data = new T();
        }

        protected void Notify()
        {
            Mediator.Send(new NotifyViewModelChangedRequest<T>());
        }

        public virtual void Handle(InterfaceRequest<T> request)
        {
            request.PropertyInfo.SetValue(Data, request.Value);

            var handlerMethod = GetType()
                .GetMethods()
                .SingleOrDefault(m => m.Name == $"{nameof(Handle)}{request.PropertyInfo.Name}");

            if (handlerMethod != null)
                handlerMethod.Invoke(this, new object[] { request });
        }
    }
}