using FrigidRogue.MonoGame.Core.Interfaces.Services;

namespace FrigidRogue.MonoGame.Core.Interfaces.Components
{
    public interface ISaveable
    {
        void SaveState(ISaveGameService saveGameService);
        void LoadState(ISaveGameService saveGameService);
    }
}