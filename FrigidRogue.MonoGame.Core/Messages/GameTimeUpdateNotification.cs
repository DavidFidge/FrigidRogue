using FrigidRogue.MonoGame.Core.Services;

using MediatR;

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