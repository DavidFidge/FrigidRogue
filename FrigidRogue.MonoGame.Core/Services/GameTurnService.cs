using System;
using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using MonoGame.Extended;

using NGenerics.Extensions;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class GameTurnService : BaseComponent, IGameTurnService, ITurnNumber
    {
        public int TurnNumber { get; set; }
        public int SequenceNumber { get; set; }

        private Dictionary<int, List<int>> _turnNumbersToSequenceNumbers = new Dictionary<int, List<int>>();
        private Dictionary<int, int> _sequenceNumbersToTurnNumber = new Dictionary<int, int>();

        public GameTurnService()
        {
            Reset();
        }

        public void SaveGame(ISaveGameStore saveGameStore)
        {
            var memento = new Memento<GameTurnService>(this);

            saveGameStore.SaveToStore(memento);
        }

        public void LoadGame(ISaveGameStore saveGameStore)
        {
            var gameTurnServiceData = saveGameStore.GetFromStore<GameTurnService>();

            TurnNumber = gameTurnServiceData.State.TurnNumber;
            SequenceNumber = gameTurnServiceData.State.SequenceNumber;
            _turnNumbersToSequenceNumbers = gameTurnServiceData.State._turnNumbersToSequenceNumbers;
            _sequenceNumbersToTurnNumber = gameTurnServiceData.State._sequenceNumbersToTurnNumber;
        }

        public void Reset()
        {
            TurnNumber = 0;
            SequenceNumber = 0;
            _turnNumbersToSequenceNumbers.Clear();
            _sequenceNumbersToTurnNumber.Clear();

            NextTurn();
        }

        public int NextTurn()
        {
            TurnNumber += 1;

            _turnNumbersToSequenceNumbers.Add(TurnNumber, new List<int>());

            return TurnNumber;
        }

        public int NextSequenceNumber()
        {
            SequenceNumber += 1;

            _turnNumbersToSequenceNumbers[TurnNumber].Add(SequenceNumber);
            _sequenceNumbersToTurnNumber.Add(SequenceNumber, TurnNumber);

            return SequenceNumber;
        }

        public int GetTurnNumberFromSequenceNumber(int sequenceNumber)
        {
            if (!_sequenceNumbersToTurnNumber.ContainsKey(sequenceNumber))
                throw new ArgumentException(
                    $"Sequence number {sequenceNumber} has not yet been reached",
                    nameof(sequenceNumber)
                );

            return _sequenceNumbersToTurnNumber[sequenceNumber];
        }

        public Range<int> GetSequenceNumberRangeForTurn(int turnNumber)
        {
            if (!_turnNumbersToSequenceNumbers.ContainsKey(turnNumber))
                throw new ArgumentException($"Turn number {turnNumber} has not yet been reached", nameof(turnNumber));

            if (_turnNumbersToSequenceNumbers[turnNumber].IsEmpty())
                return new Range<int>(0);

            return new Range<int>(
                _turnNumbersToSequenceNumbers[turnNumber].Min(),
                _turnNumbersToSequenceNumbers[turnNumber].Max()
            );
        }

        public void Populate(ITurnNumber turnNumber)
        {
            turnNumber.TurnNumber = TurnNumber;
            turnNumber.SequenceNumber = SequenceNumber;
        }
    }
}