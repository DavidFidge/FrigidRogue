using AutoMapper;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    /// <summary>
    /// For single entities, use ISaveable. For multiple entities in a list, use this to get the state of each entity
    /// then save the results as a list to the save store.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMementoState<T>
    {
        IMemento<T> GetSaveState(IMapper mapper);
        void SetLoadState(IMemento<T> memento, IMapper mapper);
    }
}