using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand<T> : BaseCommand, IMementoState<T>
    {
        public CommandResult CommandResult { get; private set; }

        public TurnDetails TurnDetails { get; set; } = new TurnDetails();

        /// <summary>
        /// Whether to advance the sequence number when calling execute. You only want to advance the
        /// sequence number when the game is being played, not during a replay.
        /// </summary>
        private bool _advanceSequenceNumber = true;

        public IGameTurnService GameTurnService { get; set; }

        public override CommandResult Execute()
        {
            if (_advanceSequenceNumber && GameTurnService != null)
            {
                GameTurnService.NextSequenceNumber();
                GameTurnService.Populate(TurnDetails);
            }

            _advanceSequenceNumber = false;

            return ExecuteInternal();
        }

        public virtual void SetLoadState(IMemento<T> memento, IMapper mapper)
        {
            _advanceSequenceNumber = false;
        }

        public abstract IMemento<T> GetSaveState(IMapper mapper);

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