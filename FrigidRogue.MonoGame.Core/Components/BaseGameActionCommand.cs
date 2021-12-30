using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameActionCommand<T> : BaseStatefulCommand<T>
    {
        public TurnDetails TurnDetails { get; set; } = new TurnDetails();
        private bool _advanceSequenceNumber = true;

        public IGameTurnService GameTurnService { get; set; }

        public override void Execute()
        {
            if (_advanceSequenceNumber && GameTurnService != null)
            {
                GameTurnService.NextSequenceNumber();
                GameTurnService.Populate(TurnDetails);
            }

            _advanceSequenceNumber = false;
        }

        public override void SetLoadState(IMemento<T> memento, IMapper mapper)
        {
            _advanceSequenceNumber = false;
        }
    }
}