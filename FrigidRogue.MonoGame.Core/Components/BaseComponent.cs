using FrigidRogue.MonoGame.Core.Interfaces.Components;
using MediatR;

using Serilog;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseComponent
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }
        public IGameProvider GameProvider { get; set; }
        public IGame Game => GameProvider.Game;
    }
}