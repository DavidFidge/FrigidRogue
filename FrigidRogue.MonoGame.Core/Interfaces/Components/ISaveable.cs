using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface ISaveable
    {
        void SaveState(ISaveGameStore saveGameStore);
        void LoadState(ISaveGameStore saveGameStore);
    }
}