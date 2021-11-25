using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface ISaveable
    {
        void SaveGame(ISaveGameStore saveGameStore);
        void LoadGame(ISaveGameStore saveGameStore);
    }
}