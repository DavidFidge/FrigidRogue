using System.IO;
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
            return SaveGameResult.Success;
        }

        public SaveGameResult SaveBytesToFile(byte[] headerBytes, byte[] saveGameBytes, string saveGameName, bool overwrite)
        {
            var saveGameFile = GetSaveGameFile(saveGameName);

            if (!overwrite && File.Exists(saveGameFile))
                return SaveGameResult.Overwrite;

            try
            {
                var headerByteCount = BitConverter.GetBytes(headerBytes.Length);

                var allBytes = headerByteCount
                    .Concat(headerBytes)
                    .Concat(saveGameBytes)
                    .ToArray();

                File.WriteAllBytes(saveGameFile, allBytes);
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
                var bytes = GetSaveGameBytesFromFile(saveGameName, saveGameFolder);
                return new LoadGameResult { Bytes = bytes };
            }
            catch (Exception e)
            {
                var message = $"Could not load game {saveGameName}";
                Logger.Error(e, message);

                return new LoadGameResult { ErrorMessage = $"{message}: {e.Message}" };
            }
        }

        private byte[] GetSaveGameBytesFromFile(string saveGameName, string saveGameFolder)
        {
            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            using var file = File.OpenRead(saveGameFile);
            using var fileBinary = new BinaryReader(file);

            GetHeaderBytes(fileBinary);

            var saveGameBytes = fileBinary.ReadBytes((int)file.Length);

            fileBinary.Close();
            file.Close();

            return saveGameBytes;
        }

        public LoadGameHeaderResult LoadHeaderBytesFromFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            try
            {
                var bytes = GetHeaderBytesFromFile(saveGameName, saveGameFolder);
                return new LoadGameHeaderResult { Bytes = bytes };
            }
            catch (Exception e)
            {
                var message = $"Could not load header for game {saveGameName}";
                Logger.Error(e, message);

                return new LoadGameHeaderResult { ErrorMessage = $"{message}: {e.Message}" };
            }
        }

        private byte[] GetHeaderBytesFromFile(string saveGameName, string saveGameFolder)
        {
            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            using var file = File.OpenRead(saveGameFile);
            using var fileBinary = new BinaryReader(file);

            var headerBytes = GetHeaderBytes(fileBinary);

            fileBinary.Close();
            file.Close();

            return headerBytes;
        }

        private byte[] GetHeaderBytes(BinaryReader fileBinary)
        {
            var headerSizeInBytes = fileBinary.ReadInt32();
            var headerBytes = fileBinary.ReadBytes(headerSizeInBytes);
            return headerBytes;
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