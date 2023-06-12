using FrigidRogue.MonoGame.Core.Services;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Interfaces.Services
{
    public interface IGameTimeService
    {
        void Reset();
        void Update(GameTime gameTime);
        void PauseGame();
        void ResumeGame();
        void IncreaseGameSpeed();
        void DecreaseGameSpeed();

        GameTime OriginalGameTime { get; }
        CustomGameTime GameTime { get; }
        bool IsPaused { get; }
        int GameSpeedPercent { get; }
        void Start();
        void Stop();
        void SaveState(ISaveGameService saveGameService);
        void LoadState(ISaveGameService saveGameService);
    }
}