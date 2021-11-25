using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public class GameProvider : IGameProvider
    {
        public IGame Game { get; set; }
    }
}