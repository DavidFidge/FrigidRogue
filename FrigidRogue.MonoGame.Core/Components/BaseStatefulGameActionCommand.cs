using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseStatefulGameActionCommand<T> : BaseGameActionCommand, IMementoState<T>
    {
        public abstract void SetLoadState(IMemento<T> memento);
        public abstract IMemento<T> GetSaveState();
    }
}