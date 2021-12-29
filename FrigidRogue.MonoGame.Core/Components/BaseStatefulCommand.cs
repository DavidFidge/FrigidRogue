using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseStatefulCommand<T> : BaseCommand, IMementoState<T>
    {
        public abstract IMemento<T> GetState(IMapper mapper);
        public abstract void SetState(IMemento<T> state, IMapper mapper);
    }
}