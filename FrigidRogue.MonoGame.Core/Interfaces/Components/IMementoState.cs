using AutoMapper;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface IMementoState<T>
    {
        IMemento<T> GetState(IMapper mapper);
        void SetState(IMemento<T> state, IMapper mapper);
    }
}