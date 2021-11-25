using DavidFidge.MonoGame.Core.Interfaces.Services;

namespace DavidFidge.MonoGame.Core.Interfaces.Components
{
    public interface ISaveable
    {
        void SaveGame(ISaveGameStore saveGameStore);
        void LoadGame(ISaveGameStore saveGameStore);
    }
}