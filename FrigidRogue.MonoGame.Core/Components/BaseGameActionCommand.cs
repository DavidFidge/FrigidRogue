namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand<T> : BaseStatefulCommand<T>
    {
        public int TurnNumber { get; private set; }
        public int SequenceNumber { get; private set; }
    }
}