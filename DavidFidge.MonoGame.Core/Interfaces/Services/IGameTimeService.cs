using DavidFidge.MonoGame.Core.Interfaces.Components;
using DavidFidge.MonoGame.Core.Services;

using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Interfaces.Services
{
    public interface IGameTimeService : ISaveable
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
    }
}