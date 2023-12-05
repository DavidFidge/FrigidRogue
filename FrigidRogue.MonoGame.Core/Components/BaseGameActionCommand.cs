using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand : BaseCommand
    {
        public CommandResult CommandResult { get; private set; }

        public TurnDetails TurnDetails { get; set; } = new TurnDetails();

        public uint Id { get; set; }
        
        /// <summary>
        /// Whether to advance the sequence number when calling execute. You only want to advance the
        /// sequence number when the game is being played, not during a replay.
        /// </summary>
        protected bool AdvanceSequenceNumber = true;

        public bool InterruptsMovement { get; set; }
        public bool RequiresPlayerInput { get; set; }
        public bool EndsPlayerTurn { get; set; }
        public bool PersistForReplay { get; set; }
        
        public IGameTurnService GameTurnService { get; set; }

        public override CommandResult Execute()
        {
            if (AdvanceSequenceNumber && GameTurnService != null)
            {
                GameTurnService.NextSequenceNumber();
                GameTurnService.Populate(TurnDetails);
            }

            AdvanceSequenceNumber = false;

            return ExecuteInternal();
        }

        protected abstract CommandResult ExecuteInternal();

        protected CommandResult Result(CommandResult commandResult)
        {
            CommandResult = commandResult;

            return CommandResult;
        }

        public override void Undo()
        {
            if (CommandResult != null)
            {
                foreach (var command in CommandResult.SubsequentCommands)
                {
                    command.Undo();
                }
            }

            UndoInternal();
        }

        protected abstract void UndoInternal();
    }
}