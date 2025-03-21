namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseCommand : BaseComponent
    {
        public abstract CommandResult Execute();

        public virtual void Undo()
        {
        }
    }
}