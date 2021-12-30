using System.Collections.Generic;

using AutoMapper;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameStore : IGameStore
    {
        /// <summary>
        /// Clear all existing data from the in memory store;
        /// </summary>
        void Clear();

        /// <summary>
        /// See if the game can be saved to file. Will return Success if it can or Overwrite if it needs permission to overwrite the existing save.
        /// </summary>
        /// <param name="saveGameName"></param>
        /// <returns></returns>
        SaveGameResult CanSaveStoreToFile(string saveGameName);

        /// <summary>
        /// Writes the save game memory store to disk
        /// </summary>
        /// <param name="saveGameName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite);

        /// <summary>
        /// Loads a save game from file into memory. You then use GetFromStore to get specific objects from the memory store.
        /// </summary>
        /// <param name="saveGameName"></param>
        /// <returns>Result from loading the game</returns>
        LoadGameResult LoadStoreFromFile(string saveGameName);

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
        /// Gets a list of games that can be loaded.
        /// </summary>
        /// <returns>List of games that can be loaded with details of each</returns>
        IList<LoadGameDetails> GetLoadGameList();

        /// <summary>
        /// The adapter which will convert an object used in the game to a data-only object which can be serialised.
        /// It must be able to convert both ways.
        /// Currently we use AutoMapper.
        /// </summary>
        IMapper Mapper { get; }
    }
}