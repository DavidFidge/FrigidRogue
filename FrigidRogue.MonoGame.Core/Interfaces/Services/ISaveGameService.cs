using System.Collections.Generic;
using AutoMapper;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameService
    {
        /// <summary>
        /// Gets an object from the store and returns it.
        /// </summary>
        /// <typeparam name="T">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <returns>data-only object wrapped in memento</returns>
        IMemento<T> GetFromStore<T>();

        /// <summary>
        /// Writes an object to the in memory save game store
        /// </summary>
        /// <typeparam name="T">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <param name="memento">data-only object wrapped in memento</param>
        void SaveToStore<T>(IMemento<T> memento);

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
        /// Clear all existing data from the in memory store;
        /// </summary>
        void Clear();

        /// <summary>
        /// Tests whether the game can be saved without saving it.
        /// </summary>
        /// <param name="saveGameName">save game name</param>
        /// <returns>A save game result with success, or if save game exists then a result with overwrite.</returns>
        SaveGameResult CanSaveStoreToFile(string saveGameName);

        /// <summary>
        /// Saves game to file. If an exception is thrown it is caught and returned in the SaveGameResult object ErrorMessage string.
        /// </summary>
        /// <param name="saveGameName">Name of save game</param>
        /// <param name="overwrite">Set to true to allow any existing game to be overwritten. If not set, SaveGameResult will return with a result of Overwrite without saving the game.</param>
        /// <returns>SaveGameResult holding results of saving the file</returns>
        SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite);

        /// <summary>
        /// Loads a game from file. Any exceptions are caught and returned in the LoadGameResult ErrorMessage
        /// </summary>
        /// <param name="saveGameName">Game to load</param>
        /// <returns>LoadGameResult holding the results of loading the file, including the loaded game bytes if load was successful</returns>
        LoadGameResult LoadStoreFromFile(string saveGameName);

        /// <summary>
        /// Gets a list of games that can be loaded
        /// </summary>
        /// <returns>Details of games that can be loaded</returns>
        IList<LoadGameDetails> GetLoadGameList();
    }
}