using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand<T> : BaseStatefulCommand<T>
    {
        public TurnDetails TurnDetails { get; set; } = new TurnDetails();
    }
}