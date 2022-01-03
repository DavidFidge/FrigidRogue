using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameFileWriter : BaseComponent, ISaveGameFileWriter
    {
        public SaveGameResult CanSaveStoreToFile(string saveGameName)
        {
            var saveGameFile = GetSaveGameFile(saveGameName);

            if (File.Exists(saveGameFile))
                return SaveGameResult.Overwrite;
            else
                return SaveGameResult.Success;
        }

        public SaveGameResult SaveBytesToFile(byte[] saveGameBytes, string saveGameName, bool overwrite)
        {
            var saveGameFile = GetSaveGameFile(saveGameName);

            if (!overwrite && File.Exists(saveGameFile))
                return SaveGameResult.Overwrite;

            try
            {
                File.WriteAllBytes(saveGameFile, saveGameBytes);
            }
            catch (Exception e)
            {
                var messageTemplate = $"Error when saving game to filesystem. Filename: {saveGameFile}";

                Logger.Error(e, messageTemplate);
                return new SaveGameResult { ErrorMessage = $"{messageTemplate} - {e.Message}" };
            }

            return SaveGameResult.Success;
        }

        public string GetSaveGameFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");
            return saveGameFile;
        }

        public FileInfo GetFileInfo(string saveGameName)
        {
            var saveGameFile = GetSaveGameFile(saveGameName);

            return new FileInfo(saveGameFile);
        }

        public LoadGameResult LoadBytesFromFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            try
            {
                var bytes = GetBytesFromFile(saveGameName, saveGameFolder);
                return new LoadGameResult { Bytes = bytes };
            }
            catch (Exception e)
            {
                var message = $"Could not load game {saveGameName}";
                Logger.Error(e, message);

                return new LoadGameResult { ErrorMessage = $"{message}: {e.Message}" };
            }
        }

        private byte[] GetBytesFromFile(string saveGameName, string saveGameFolder)
        {
            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            var saveGameBytes = File.ReadAllBytes(saveGameFile);
            return saveGameBytes;
        }

        public string GetSaveGamePath()
        {
            var localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var gameFolder = Path.Combine(localFolderPath, Assembly.GetEntryAssembly().GetName().Name);

            if (!Directory.Exists(gameFolder))
                Directory.CreateDirectory(gameFolder);

            var saveGameFolder = Path.Combine(gameFolder, "Saved Games");

            if (!Directory.Exists(saveGameFolder))
                Directory.CreateDirectory(saveGameFolder);

            return saveGameFolder;
        }

        public IList<string> GetSavedGameFileNames()
        {
            var saveGamePath = GetSaveGamePath();

            var directoryInfo = new DirectoryInfo(saveGamePath);

            var fileInfos = directoryInfo.GetFiles("*.sav");

            return fileInfos
                .Select(fi => Path.GetFileNameWithoutExtension(fi.Name))
                .ToList();
        }
    }
}