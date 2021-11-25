using DavidFidge.MonoGame.Core.Interfaces.Components;

namespace DavidFidge.MonoGame.Core.Interfaces.Services
{
    public interface IGameStore
    {
        IMemento<T> GetFromStore<T>();
        void SaveToStore<T>(IMemento<T> memento);
    }
}