using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseStatefulCommand<T> : BaseCommand, IMementoState<T>
    {
        public abstract IMemento<T> GetSaveState(IMapper mapper);
        public abstract void SetLoadState(IMemento<T> memento, IMapper mapper);
    }
}