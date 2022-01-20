namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseCommand : BaseComponent
    {
        public abstract CommandResult Execute();
        public abstract void Undo();
    }
}