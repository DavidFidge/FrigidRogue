using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

        public SaveGameStore()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
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

        public void SaveStoreToFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            var saveGameString = JsonConvert.SerializeObject(_jsonObjectStore, _jsonSerializerSettings);

            var saveGameBytes = GZipStream.CompressString(saveGameString);

            File.WriteAllBytes(saveGameFile, saveGameBytes);
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