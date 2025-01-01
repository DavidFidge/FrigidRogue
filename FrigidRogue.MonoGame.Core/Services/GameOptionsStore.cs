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
        private readonly Dictionary<Type, object> _cache = new();

        public GameOptionsStore()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
        }

        public IMemento<T> GetFromStore<T>()
        {
            if (_cache.ContainsKey(typeof(T)))
                return new Memento<T>((T)_cache[typeof(T)]);

            var gameOptionsFile = GetGameOptionsFilePath<T>();

            if (!File.Exists(gameOptionsFile))
                return null;

            var jsonString = File.ReadAllText(gameOptionsFile);

            var option = new Memento<T>(JsonConvert.DeserializeObject<T>(jsonString, _jsonSerializerSettings));

            _cache.Add(typeof(T), option.State);

            return option;
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            var gameOptionsFile = GetGameOptionsFilePath<T>();

            var jsonString = JsonConvert.SerializeObject(memento.State, _jsonSerializerSettings);

            File.WriteAllText(gameOptionsFile, jsonString);

            // Invalidate the cache.  Another way would be to update the cache if it exists, but this way has the advantage where
            // T does not need to be cloned or properties copied to ensure that a copy of the state is saved rather than the
            // object itself (the object could later be modified by the game code thus causing the cache data to be modified too
            // if it used the same object).  When another get is done, it is read from file and thus we have a new copy of T.
            if (_cache.ContainsKey(typeof(T)))
                _cache.Remove(typeof(T));
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