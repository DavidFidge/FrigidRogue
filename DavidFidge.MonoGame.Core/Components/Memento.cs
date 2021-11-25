using DavidFidge.MonoGame.Core.Interfaces.Components;

namespace DavidFidge.MonoGame.Core.Services
{
    public class Memento<T> : IMemento<T>
    {
        public Memento(T state)
        {
            State = state;
        }

        public Memento()
        {
        }

        public T State { get; set; }
    }
}