using AutoMapper;
using FrigidRogue.MonoGame.Core.Services;
using SharpDX.Direct3D9;

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
        void LoadStoreFromFile(string saveGameName);

        /// <summary>
        /// Gets a specific object from the in memory save game storage. This method handles the conversion for you (the default
        /// implementation uses AutoMapper).
        /// Objects are keyed by their type so you can only have one object per type.
        /// Use a list of objects if you need to store multiple objects for a type.
        /// </summary>
        /// <typeparam name="T">Game object</typeparam>
        /// <typeparam name="TSaveData">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <returns></returns>
        T GetFromStore<T, TSaveData>();

        /// <summary>
        /// Gets a specific object from the in memory save game storage. This method handles the conversion for you (the default
        /// implementation uses AutoMapper). This version allows you to pass an existing destination object who's values will get
        /// overwritten with that from the save game store.
        /// Objects are keyed by their type so you can only have one object per type.
        /// Use a list of objects if you need to store multiple objects for a type.
        /// </summary>
        /// <typeparam name="T">Game object</typeparam>
        /// <typeparam name="TSaveData">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <returns></returns>
        T GetFromStore<T, TSaveData>(T existingObject);

        /// <summary>
        /// Writes an object to the in memory save game store. This method handles the conversion for you (the default
        /// implementation uses AutoMapper).
        /// </summary>
        /// <typeparam name="T">An object used in the game</typeparam>
        /// <typeparam name="TSaveData">A data-only object which can be serialised (default implementation uses json)</typeparam>
        /// <param name="item"></param>
        void SaveToStore<T, TSaveData>(T item);

        /// <summary>
        /// The adapter which will convert an object used in the game to a data-only object which can be serialised.
        /// It must be able to convert both ways.
        /// Currently we use AutoMapper.
        /// </summary>
        IMapper Mapper { get; }
    }
}