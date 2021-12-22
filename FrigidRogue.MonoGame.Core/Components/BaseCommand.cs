namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseCommand : BaseComponent
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}