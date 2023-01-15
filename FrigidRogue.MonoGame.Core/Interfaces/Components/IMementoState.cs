namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    /// <summary>
    /// Helper interface that can be used on abstract types to retrieve a memento for the concrete type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMementoState<T>
    {
        IMemento<T> GetSaveState();
        void SetLoadState(IMemento<T> memento);
    }
}