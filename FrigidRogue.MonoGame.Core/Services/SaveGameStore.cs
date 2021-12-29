using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using AutoMapper;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using MonoGame.Framework.Utilities.Deflate;

using Newtonsoft.Json;
using NGenerics.Extensions;

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

        public void GetFromStore<TSaveData>(IMementoState<TSaveData> existingObject)
        {
            var memento = GetFromStore<TSaveData>();

            existingObject.SetState(memento, Mapper);
        }

        private T GetFromStore<T>(Dictionary<Type, string> jsonStore)
        {
            var key = jsonStore.Keys.FirstOrDefault(k => typeof(ILoadGameDetail).IsAssignableFrom(k));

            if (key == null)
                throw new Exception($"An object was not found in the store which is or can be assigned as a type {typeof(T)}");

            var jsonString = jsonStore[key];

            return (T)JsonConvert.DeserializeObject(jsonString, key, _jsonSerializerSettings);
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            var jsonString = JsonConvert.SerializeObject(memento.State, _jsonSerializerSettings);

            _jsonObjectStore.Add(typeof(T), jsonString);
        }

        public void SaveToStore<TSaveData>(IMementoState<TSaveData> item)
        {
            var state = item.GetState(Mapper);

            SaveToStore(state);
        }

        public IList<IMemento<TSaveData>> GetListFromStore<TSaveData>()
        {
            var jsonString = _jsonObjectStore[typeof(IList<TSaveData>)];

            var list = JsonConvert.DeserializeObject<IList<TSaveData>>(jsonString, _jsonSerializerSettings);

            return list.Select(i => new Memento<TSaveData>(i)).Cast<IMemento<TSaveData>>().ToList();
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

        public LoadGameResult LoadStoreFromFile(string saveGameName)
        {
            var saveGameFolder = GetSaveGamePath();

            try
            {
                var deserialisedStore = DeserialiseStoreFromFile(saveGameName, saveGameFolder);

                _jsonObjectStore = deserialisedStore;
            }
            catch (Exception e)
            {
                var message = $"Could not load game {saveGameName}";
                Logger.Error(e, message);

                return new LoadGameResult { ErrorMessage = $"{message}: {e.Message}" };
            }

            return LoadGameResult.Success;
        }

        private Dictionary<Type, string> DeserialiseStoreFromFile(string saveGameName, string saveGameFolder)
        {
            var saveGameFile = Path.Combine(saveGameFolder, $"{saveGameName}.sav");

            var saveGameBytes = File.ReadAllBytes(saveGameFile);

            var saveGameString = GZipStream.UncompressString(saveGameBytes);

            var deserialisedStore =
                JsonConvert.DeserializeObject<Dictionary<Type, string>>(saveGameString, _jsonSerializerSettings);

            return deserialisedStore;
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

        public IList<LoadGameDetails> GetLoadGameList()
        {
            var saveGamePath = GetSaveGamePath();

            var directoryInfo = new DirectoryInfo(saveGamePath);

            var fileInfos = directoryInfo.GetFiles("*.sav");

            var loadGameList = new List<LoadGameDetails>(fileInfos.Length);

            foreach (var fileInfo in fileInfos)
            {
                Dictionary<Type, string> deserialisedFile;

                try
                {
                    deserialisedFile = DeserialiseStoreFromFile(Path.GetFileNameWithoutExtension(fileInfo.Name), saveGamePath);
                }
                catch (Exception e)
                {
                    Logger.Warning(e, $"Exception occurred when deserialising file {fileInfo.Name}");
                    continue;
                }

                ILoadGameDetail loadGameDetails;

                try
                {
                    loadGameDetails = GetFromStore<ILoadGameDetail>(deserialisedFile);
                }
                catch (Exception e)
                {
                    Logger.Warning(e, $"Exception occurred when getting ILoadGameDetail from save file {fileInfo.Name}");
                    continue;
                }

                loadGameList.Add(
                    new LoadGameDetails
                    {
                        DateTime = fileInfo.LastWriteTime,
                        Filename = Path.GetFileNameWithoutExtension(fileInfo.Name),
                        LoadGameDetail = loadGameDetails.LoadGameDetail
                    }
                );
            }

            return loadGameList;
        }
    }
}