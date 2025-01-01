using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Services;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class GameTimeUpdateNotification : INotification
    {
        public CustomGameTime GameTime { get; }

        public GameTimeUpdateNotification(CustomGameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}