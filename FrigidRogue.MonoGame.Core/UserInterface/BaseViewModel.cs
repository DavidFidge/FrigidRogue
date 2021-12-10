using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Messages;
using MediatR;

namespace FrigidRogue.MonoGame.Core.UserInterface
{
    public abstract class BaseViewModel<T> : BaseComponent, IRequestHandler<InterfaceRequest<T>>
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

        public Task<Unit> Handle(InterfaceRequest<T> request, CancellationToken cancellationToken)
        {
            request.PropertyInfo.SetValue(Data, request.Value);

            var handlerMethod = GetType()
                .GetMethods()
                .SingleOrDefault(m => m.Name == $"{nameof(Handle)}{request.PropertyInfo.Name}");

            if (handlerMethod != null)
                handlerMethod.Invoke(this, new object[] { request, cancellationToken });

            return Unit.Task;
        }
    }
}