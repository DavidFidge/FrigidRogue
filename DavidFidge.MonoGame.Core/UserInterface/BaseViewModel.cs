using DavidFidge.MonoGame.Core.Components;
using DavidFidge.MonoGame.Core.Messages;

namespace DavidFidge.MonoGame.Core.UserInterface
{
    public abstract class BaseViewModel<T> : BaseComponent
        where T : new()
    {
        public T Data { get; protected set; }

        public virtual void Initialize()
        {
            Data = new T();
        }

        protected void Notify()
        {
            Mediator.Send(new UpdateViewRequest<T>());
        }
    }
}