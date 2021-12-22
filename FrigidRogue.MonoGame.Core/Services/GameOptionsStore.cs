using System;
using System.IO;
using System.Reflection;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using Newtonsoft.Json;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class GameOptionsStore : IGameOptionsStore
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public GameOptionsStore()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
        }

        public IMemento<T> GetFromStore<T>()
        {
            var gameOptionsFile = GetGameOptionsFilePath<T>();

            if (!File.Exists(gameOptionsFile))
                return null;

            var jsonString = File.ReadAllText(gameOptionsFile);

            return new Memento<T>(JsonConvert.DeserializeObject<T>(jsonString, _jsonSerializerSettings));
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            var gameOptionsFile = GetGameOptionsFilePath<T>();

            var jsonString = JsonConvert.SerializeObject(memento.State, _jsonSerializerSettings);

            File.WriteAllText(gameOptionsFile, jsonString);
        }

        private string GetGameOptionsFilePath<T>()
        {
            var localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var gameFolder = Path.Combine(localFolderPath, Assembly.GetEntryAssembly().GetName().Name);

            if (!Directory.Exists(gameFolder))
                Directory.CreateDirectory(gameFolder);

            var optionsFolder = Path.Combine(gameFolder, "Options");

            if (!Directory.Exists(optionsFolder))
                Directory.CreateDirectory(optionsFolder);

            var optionsFile = Path.Combine(optionsFolder, $"{typeof(T).Name}.txt");

            return optionsFile;
        }
    }
}