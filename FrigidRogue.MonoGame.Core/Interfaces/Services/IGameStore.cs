using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface IGameStore
    {
        /// <summary>
        /// Gets an object from the store and returns it.
        /// </summary>
        /// <typeparam name="T">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <returns>data-only object wrapped in memento</returns>
        IMemento<T> GetFromStore<T>();

        /// <summary>
        /// Writes an object to the in memory save game store
        /// </summary>
        /// <typeparam name="T">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <param name="memento">data-only object wrapped in memento</param>
        void SaveToStore<T>(IMemento<T> memento);
    }
}