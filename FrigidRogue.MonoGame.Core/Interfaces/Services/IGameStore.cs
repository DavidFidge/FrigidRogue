using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface IGameStore
    {
        IMemento<T> GetFromStore<T>();
        void SaveToStore<T>(IMemento<T> memento);
    }
}