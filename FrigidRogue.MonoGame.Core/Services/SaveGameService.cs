using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Services
{
    public class SaveGameService : BaseComponent, ISaveGameService
    {
        private readonly ISaveGameStore _saveGameStore;
        private readonly ISaveGameFileWriter _saveGameFileWriter;

        public SaveGameService(ISaveGameFileWriter saveGameFileWriter)
        {
            _saveGameStore = new SaveGameStore();

            _saveGameFileWriter = saveGameFileWriter;
        }

        public IMemento<T> GetFromStore<T>()
        {
            return _saveGameStore.GetFromStore<T>();
        }

        public void SaveToStore<T>(IMemento<T> memento)
        {
            _saveGameStore.SaveToStore<T>(memento);
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
        }

        public SaveGameResult CanSaveStoreToFile(string saveGameName)
        {
            return _saveGameFileWriter.CanSaveStoreToFile(saveGameName);
        }

        public SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite)
        {
            var saveGameBytes = _saveGameStore.GetSaveGameBytes();

            return _saveGameFileWriter.SaveBytesToFile(saveGameBytes, saveGameName, overwrite);
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
                var loadGameResult = _saveGameFileWriter.LoadBytesFromFile(saveGameName);

                if (loadGameResult.Failure)
                {
                    Logger.Warning($"Error occurred when getting load game details for file {saveGameName} - {loadGameResult.ErrorMessage}");
                    continue;
                }

                var saveGameStore = new SaveGameStore();

                try
                {
                    saveGameStore.DeserialiseStoreFromBytes(loadGameResult.Bytes);
                }
                catch (Exception e)
                {
                    Logger.Warning(e, $"Exception occurred when deserialising save game from bytes for save file {saveGameName}");

                    continue;
                }

                IMemento<ILoadGameDetail> loadGameDetails;

                try
                {
                    loadGameDetails = saveGameStore.GetFromStore<ILoadGameDetail>();
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
