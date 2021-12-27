using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameStore : IGameStore
    {
        SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite);
        void LoadStoreFromFile(string saveGameName);
    }
}