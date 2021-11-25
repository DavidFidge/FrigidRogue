using DavidFidge.MonoGame.Core.Services;

using MediatR;

namespace DavidFidge.MonoGame.Core.Messages
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