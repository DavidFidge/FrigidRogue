using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Services
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

        public static IMemento<T> CreateWithAutoMapper<TItem>(TItem item, IMapper mapper)
        {
            var state = mapper.Map<TItem, T>(item);

            return new Memento<T>(state);
        }

        public static void SetWithAutoMapper<TItem>(TItem item, IMemento<T> memento, IMapper mapper)
        {
            mapper.Map(memento.State, item);
        }
    }
}