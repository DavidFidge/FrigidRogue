using System.IO;

using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameFileWriter
    {
        /// <summary>
        /// Tests whether the game can be saved without saving it.
        /// </summary>
        /// <param name="saveGameName">save game name</param>
        /// <returns>A save game result with success, or if save game exists then a result with overwrite.</returns>
        SaveGameResult CanSaveStoreToFile(string saveGameName);

        /// <summary>
        /// Saves game to file. If an exception is thrown it is caught and returned in the SaveGameResult object ErrorMessage string.
        /// </summary>
        /// <param name="headerBytes">Game header which contains info to show in load game list</param>
        /// <param name="saveGameBytes">Game to save to file as bytes</param>
        /// <param name="saveGameName">Name of save game</param>
        /// <param name="overwrite">Set to true to allow any existing game to be overwritten. If not set, SaveGameResult will return with a result of Overwrite without saving the game.</param>
        /// <returns>SaveGameResult holding results of saving the file</returns>
        SaveGameResult SaveBytesToFile(byte[] headerBytes, byte[] saveGameBytes, string saveGameName, bool overwrite);

        /// <summary>
        /// Loads a game from file. Any exceptions are caught and returned in the LoadGameResult ErrorMessage
        /// </summary>
        /// <param name="saveGameName">Game to load</param>
        /// <returns>LoadGameResult holding the results of loading the file, including the loaded game bytes if load was successful</returns>
        LoadGameResult LoadBytesFromFile(string saveGameName);

        /// <summary>
        /// Gets a list of saved games that can be loaded
        /// </summary>
        /// <returns>List of save game names that can be loaded</returns>
        IList<string> GetSavedGameFileNames();

        /// <summary>
        /// Gets FileInfo for a save game
        /// </summary>
        /// <param name="saveGameName">Name of save game</param>
        /// <returns>FileInfo for the save game</returns>
        FileInfo GetFileInfo(string saveGameName);

        /// <summary>
        /// Loads a game header from file. Any exceptions are caught and returned in the LoadGameResult ErrorMessage
        /// </summary>
        /// <param name="saveGameName">Game to load header from</param>
        /// <returns>LoadGameHeaderResult holding the results of loading the header for the game, including the loaded game header bytes if load was successful</returns>
        LoadGameHeaderResult LoadHeaderBytesFromFile(string saveGameName);
    }
}