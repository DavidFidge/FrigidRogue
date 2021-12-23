using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand<T> : BaseStatefulCommand<T>
    {
        public TurnDetails TurnDetails { get; set; } = new TurnDetails();
        protected bool AdvanceSequenceNumber = true;

        public IGameTurnService GameTurnService { get; set; }

        public override void Execute()
        {
            if (AdvanceSequenceNumber && GameTurnService != null)
            {
                GameTurnService.NextSequenceNumber();
                GameTurnService.Populate(TurnDetails);
            }
        }
    }
}