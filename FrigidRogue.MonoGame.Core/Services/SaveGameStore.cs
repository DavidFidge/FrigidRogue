using System.IO;
using System.Text;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;
using MonoGame.Extended.Serialization;
using Newtonsoft.Json;
using CompressionLevel = MonoGame.Framework.Utilities.Deflate.CompressionLevel;
using CompressionMode = MonoGame.Framework.Utilities.Deflate.CompressionMode;
using GZipStream = MonoGame.Framework.Utilities.Deflate.GZipStream;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameStore : ISaveGameStore
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private Dictionary<Type, object> _jsonGameObjectStore = new();

        public SaveGameStore()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                TypeNameHandling = TypeNameHandling.All
            };
            
            _jsonSerializerSettings.Converters.Add(new RangeJsonConverter<int>());
        }

        public IMemento<T> GetFromStore<T>()
        {
            var success = _jsonGameObjectStore.TryGetValue(typeof(T), out object item);

            if (success)
                return new Memento<T>((T)item);

            var key = _jsonGameObjectStore.Keys.FirstOrDefault(k => typeof(T).IsAssignableFrom(k));

            if (key == null)
                throw new Exception($"An object was not found in the store which is or can be assigned as a type {typeof(T)}");

            item = _jsonGameObjectStore[key];

            return new Memento<T>((T)item);
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            _jsonGameObjectStore.Add(typeof(T), memento.State);
        }

        public IList<IMemento<TSaveData>> GetListFromStore<TSaveData>()
        {
            var item = _jsonGameObjectStore[typeof(IList<TSaveData>)];

            var list = (IList<IMemento<TSaveData>>)item;

            return list;
        }

        public void SaveListToStore<TSaveData>(IList<IMemento<TSaveData>> item)
        {
            _jsonGameObjectStore.Add(typeof(IList<TSaveData>), item);
        }

        public void Clear()
        {
            _jsonGameObjectStore = new Dictionary<Type, object>();
        }

        public byte[] GetSaveGameBytes()
        {
            var saveGameString = JsonConvert.SerializeObject(_jsonGameObjectStore, _jsonSerializerSettings);

            var saveGameBytes = GZipStream.CompressString(saveGameString);

            return saveGameBytes;
        }

        public void DeserialiseStoreFromBytes(byte[] saveGameBytes)
        {
            var saveGameString = GZipStream.UncompressString(saveGameBytes);

            _jsonGameObjectStore = JsonConvert.DeserializeObject<Dictionary<Type, object>>(saveGameString, _jsonSerializerSettings);
        }

        // Alternative implementation for uncompressing using BestSpeed.  Currently not using as it doesn't appear to improve performance much.
        private static string UncompressString(byte[] saveGameBytes)
        {
            var buffer = new byte[1024];
            var utF8 = Encoding.UTF8;
            using var memoryStream = new MemoryStream();
            using var decompressorMemoryStream = new MemoryStream(saveGameBytes);
            using var decompressor = (Stream) new GZipStream(decompressorMemoryStream, CompressionMode.Decompress, CompressionLevel.BestSpeed);

                int count;
                while ((count = decompressor.Read(buffer, 0, buffer.Length)) != 0)
                    memoryStream.Write(buffer, 0, count);

            memoryStream.Seek(0L, SeekOrigin.Begin);
            var result = new StreamReader((Stream) memoryStream, utF8).ReadToEnd();

            decompressorMemoryStream.Close();
            decompressor.Close();
            memoryStream.Close();

            return result;
        }

        // Alternative implementation for compressing using BestSpeed.  Currently not using as it doesn't appear to improve performance much
        private static byte[] CompressString(string saveGameString)
        {
            var utf8Encoding = Encoding.UTF8;
            var bytes = utf8Encoding.GetBytes(saveGameString);

            using var memoryStream = new MemoryStream();
            using var compressor = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestSpeed);

            compressor.Write(bytes, 0, bytes.Length);
            compressor.Close();
            memoryStream.Close();

            var saveGameBytes = memoryStream.ToArray();
            return saveGameBytes;
        }
    }
}
