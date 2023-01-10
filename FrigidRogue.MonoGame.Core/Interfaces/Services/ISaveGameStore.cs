using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameStore : IGameStore
    {
        /// <summary>
        /// Clear all existing data from the in memory store;
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets a list of objects from the in memory store
        /// </summary>
        /// <typeparam name="TSaveData">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <returns>list of data-only objects wrapped in memento</returns>
        IList<IMemento<TSaveData>> GetListFromStore<TSaveData>();

        /// <summary>
        /// Writes an object to the in memory store
        /// </summary>
        /// <typeparam name="TSaveData">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <param name="item">data-only objects wrapped in memento</param>
        void SaveListToStore<TSaveData>(IList<IMemento<TSaveData>> item);

        /// <summary>
        /// Gets the save game as a byte array
        /// </summary>
        /// <returns>byte array</returns>
        byte[] GetSaveGameBytes();

        /// <summary>
        /// Deserialises a save game from bytes and replaces any existing data in the store with the deserialised contents
        /// </summary>
        /// <param name="saveGameBytes">save game as byte array</param>
        void DeserialiseStoreFromBytes(byte[] saveGameBytes);
    }
}
