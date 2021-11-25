namespace DavidFidge.MonoGame.Core.Interfaces.Services
{
    public interface ISaveGameService
    {
        void LoadGame(ISaveGameStore saveGameStore);
        void SaveGame(ISaveGameStore saveGameStore);
    }
}