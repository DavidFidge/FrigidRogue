using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using AutoMapper;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using MonoGame.Framework.Utilities.Deflate;

using Newtonsoft.Json;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameStore : BaseComponent, ISaveGameStore
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private Dictionary<Type, string> _jsonObjectStore = new Dictionary<Type, string>();
        public IMapper Mapper { get; set; }

        public SaveGameStore()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public IMemento<T> GetFromStore<T>()
        {
            var jsonString = _jsonObjectStore[typeof(T)];

            return new Memento<T>(JsonConvert.DeserializeObject<T>(jsonString, _jsonSerializerSettings));
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            var jsonString = JsonConvert.SerializeObject(memento.State, _jsonSerializerSettings);

            _jsonObjectStore.Add(typeof(T), jsonString);
        }

        public T GetFromStore<T, TSaveData>()
        {
            var saveData = GetFromStore<TSaveData>().State;

            return Mapper.Map<TSaveData, T>(saveData);
        }

        public T GetFromStore<T, TSaveData>(T existingObject)
        {
            var saveData = GetFromStore<TSaveData>().State;

            return Mapper.Map(saveData, existingObject);
        }

        public void SaveToStore<T, TSaveData>(T item)
        {
            var saveData = Mapper.Map<T, TSaveData>(item);

            SaveToStore(new Memento<TSaveData>(saveData));
        }

        public void Clear()
        {
            _jsonObjectStore = new Dictionary<Type, string>();
        }

        public SaveGameResult CanSaveStoreToFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            if (File.Exists(saveGameFile))
                return SaveGameResult.Overwrite;
            else
                return SaveGameResult.Success;
        }

        public SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite)
        {
            var saveGameFolder = GetSaveGamePath();

            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            var saveGameString = JsonConvert.SerializeObject(_jsonObjectStore, _jsonSerializerSettings);

            var saveGameBytes = GZipStream.CompressString(saveGameString);

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

        public void LoadStoreFromFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            var saveGameBytes = File.ReadAllBytes(saveGameFile);

            var saveGameString = GZipStream.UncompressString(saveGameBytes);

            _jsonObjectStore = JsonConvert.DeserializeObject<Dictionary<Type, string>>(saveGameString, _jsonSerializerSettings);
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
    }
}