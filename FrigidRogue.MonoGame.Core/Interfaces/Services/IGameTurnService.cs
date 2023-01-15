using FrigidRogue.MonoGame.Core.Interfaces.Components;

using MonoGame.Extended;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface IGameTurnService
    {
        /// <summary>
        /// Resets all counters
        /// </summary>
        void Reset();

        /// <summary>
        /// Game turn number. A game turn is generally the distinction between PLAYER moves.
        /// </summary>
        int TurnNumber { get; }

        /// <summary>
        /// Game sequence number. Unlike the turn number, which is a distinction between player moves, this can be used as an action identifier
        /// to track every historical action in your game.  These can then be replayed in order to provide a replay facility for your players.
        /// </summary>
        int SequenceNumber { get; }

        /// <summary>
        /// Increments the turn number and returns it.
        /// </summary>
        /// <returns>Next turn number</returns>
        int NextTurn();

        /// <summary>
        /// Increments the sequence number and returns it.
        /// </summary>
        /// <returns>Next sequence number</returns>
        int NextSequenceNumber();

        /// <summary>
        /// Gets the turn number for a specific sequence number
        /// </summary>
        /// <returns>Turn number</returns>
        int GetTurnNumberFromSequenceNumber(int sequenceNumber);

        /// <summary>
        /// Gets the range of sequence numbers that were used during a turn
        /// </summary>
        /// <returns>Sequence number range</returns>
        Range<int> GetSequenceNumberRangeForTurn(int turnNumber);

        /// <summary>
        /// Populates turn details on object passed in
        /// </summary>
        /// <param name="turnNumber"></param>
        public void Populate(ITurnNumber turnNumber);
    }
}