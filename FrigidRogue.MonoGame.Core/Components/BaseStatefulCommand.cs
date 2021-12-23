using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseStatefulCommand<T> : BaseCommand
    {
        public abstract IMemento<T> GetState();
        public abstract void SetState(IMemento<T> state);
    }
}