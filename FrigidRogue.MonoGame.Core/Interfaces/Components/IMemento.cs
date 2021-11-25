namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface IMemento<T>
    {
        T State { get; set; }
    }
}