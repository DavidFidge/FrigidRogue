using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameService : BaseComponent, ISaveGameService
    {
        private readonly ISaveGameStore _saveGameStore;
        private readonly ISaveGameStore _headerStore;
        private readonly ISaveGameFileWriter _saveGameFileWriter;

        public SaveGameService(ISaveGameFileWriter saveGameFileWriter)
        {
            _saveGameStore = new SaveGameStore();
            _headerStore = new SaveGameStore();

            _saveGameFileWriter = saveGameFileWriter;
        }

        public IMemento<T> GetFromStore<T>()
        {
            return _saveGameStore.GetFromStore<T>();
        }

        public IMemento<T> GetHeaderFromStore<T>() where T : IHeaderSaveData
        {
            return _headerStore.GetFromStore<T>();
        }
        public void SaveToStore<T>(IMemento<T> memento)
        {
            _saveGameStore.SaveToStore(memento);
        }

        public void SaveHeaderToStore<T>(IMemento<T> memento) where T : IHeaderSaveData
        {
            _headerStore.SaveToStore(memento);
        }

        public IList<IMemento<TSaveData>> GetListFromStore<TSaveData>()
        {
            return _saveGameStore.GetListFromStore<TSaveData>();
        }

        public void SaveListToStore<TSaveData>(IList<IMemento<TSaveData>> item)
        {
            _saveGameStore.SaveListToStore(item);
        }

        public void Clear()
        {
            _saveGameStore.Clear();
            _headerStore.Clear();
        }

        public SaveGameResult CanSaveStoreToFile(string saveGameName)
        {
            return _saveGameFileWriter.CanSaveStoreToFile(saveGameName);
        }

        public SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite)
        {
            var saveGameBytes = _saveGameStore.GetSaveGameBytes();
            var headerBytes = _headerStore.GetSaveGameBytes();

            return _saveGameFileWriter.SaveBytesToFile(headerBytes, saveGameBytes, saveGameName, overwrite);
        }

        public LoadGameResult LoadStoreFromFile(string saveGameName)
        {
            var loadGameResult = _saveGameFileWriter.LoadBytesFromFile(saveGameName);

            if (loadGameResult.Failure)
                return loadGameResult;

            _saveGameStore.DeserialiseStoreFromBytes(loadGameResult.Bytes);

            return loadGameResult;
        }

        public IList<LoadGameDetails> GetLoadGameList()
        {
            var saveGameFilenames = _saveGameFileWriter.GetSavedGameFileNames();
            var loadGameList = new List<LoadGameDetails>();

            foreach (var saveGameName in saveGameFilenames)
            {
                var loadGameResult = _saveGameFileWriter.LoadHeaderBytesFromFile(saveGameName);

                if (loadGameResult.Failure)
                {
                    Logger.Warning($"Error occurred when getting load game header details for file {saveGameName} - {loadGameResult.ErrorMessage}");
                    continue;
                }

                try
                {
                    _headerStore.DeserialiseStoreFromBytes(loadGameResult.Bytes);
                }
                catch (Exception e)
                {
                    Logger.Warning(e, $"Exception occurred when deserialising save game header from bytes for save file {saveGameName}");

                    continue;
                }

                IMemento<IHeaderSaveData> loadGameDetails;

                try
                {
                    loadGameDetails = _headerStore.GetFromStore<IHeaderSaveData>();
                }
                catch (Exception e)
                {
                    Logger.Warning(e, $"Exception occurred when getting ILoadGameDetail from save file {saveGameName}");
                    continue;
                }

                var fileInfo = _saveGameFileWriter.GetFileInfo(saveGameName);

                loadGameList.Add(
                    new LoadGameDetails
                    {
                        DateTime = fileInfo.LastWriteTime,
                        Filename = saveGameName,
                        LoadGameDetail = loadGameDetails.State.LoadGameDetail
                    }
                );
            }

            return loadGameList;
        }
    }
}
