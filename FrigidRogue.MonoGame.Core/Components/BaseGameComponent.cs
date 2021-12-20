using FrigidRogue.MonoGame.Core.Interfaces.Components;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseGameComponent : BaseComponent
    {
        public IGameProvider GameProvider { get; set; }
        public IGame Game => GameProvider.Game;
    }
}