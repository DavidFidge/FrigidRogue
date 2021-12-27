using AutoMapper;
using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameStore : IGameStore
    {
        SaveGameResult CanSaveStoreToFile(string saveGameName);
        SaveGameResult SaveStoreToFile(string saveGameName, bool overwrite);
        void LoadStoreFromFile(string saveGameName);
        IMapper Mapper { get; }
    }
}