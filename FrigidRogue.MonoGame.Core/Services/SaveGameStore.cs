using System;
using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using MonoGame.Framework.Utilities.Deflate;

using Newtonsoft.Json;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameStore : ISaveGameStore
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private Dictionary<Type, string> _jsonObjectStore = new Dictionary<Type, string>();

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
            var success = _jsonObjectStore.TryGetValue(typeof(T), out string jsonString);

            if (success)
                return new Memento<T>(JsonConvert.DeserializeObject<T>(jsonString, _jsonSerializerSettings));

            var key = _jsonObjectStore.Keys.FirstOrDefault(k => typeof(T).IsAssignableFrom(k));

            if (key == null)
                throw new Exception($"An object was not found in the store which is or can be assigned as a type {typeof(T)}");

            jsonString = _jsonObjectStore[key];

            return new Memento<T>((T)JsonConvert.DeserializeObject(jsonString, key, _jsonSerializerSettings));
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            var jsonString = JsonConvert.SerializeObject(memento.State, _jsonSerializerSettings);

            _jsonObjectStore.Add(typeof(T), jsonString);
        }

        public IList<IMemento<TSaveData>> GetListFromStore<TSaveData>()
        {
            var jsonString = _jsonObjectStore[typeof(IList<TSaveData>)];

            var list = JsonConvert.DeserializeObject<IList<IMemento<TSaveData>>>(jsonString, _jsonSerializerSettings);

            return list;
        }

        public void SaveListToStore<TSaveData>(IList<IMemento<TSaveData>> item)
        {
            var jsonString = JsonConvert.SerializeObject(item, _jsonSerializerSettings);

            _jsonObjectStore.Add(typeof(IList<TSaveData>), jsonString);
        }

        public void Clear()
        {
            _jsonObjectStore = new Dictionary<Type, string>();
        }

        public byte[] GetSaveGameBytes()
        {
            var saveGameString = JsonConvert.SerializeObject(_jsonObjectStore, _jsonSerializerSettings);

            var saveGameBytes = GZipStream.CompressString(saveGameString);

            return saveGameBytes;
        }

        public void DeserialiseStoreFromBytes(byte[] saveGameBytes)
        {
            var saveGameString = GZipStream.UncompressString(saveGameBytes);

            _jsonObjectStore = JsonConvert.DeserializeObject<Dictionary<Type, string>>(saveGameString, _jsonSerializerSettings);
        }
    }
}