using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseStatefulGameActionCommand<T> : BaseGameActionCommand, IMementoState<T>
    {
        public virtual void SetLoadState(IMemento<T> memento, IMapper mapper)
        {
            AdvanceSequenceNumber = false;
        }

        public abstract IMemento<T> GetSaveState(IMapper mapper);
    }
}