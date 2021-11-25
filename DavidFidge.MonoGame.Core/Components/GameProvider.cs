using DavidFidge.MonoGame.Core.Interfaces.Components;

namespace DavidFidge.MonoGame.Core.Components
{
    public class GameProvider : IGameProvider
    {
        public IGame Game { get; set; }
    }
}