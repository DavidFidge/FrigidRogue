using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using Serilog;

namespace FrigidRogue.MonoGame.Core.Components
{
    public abstract class BaseComponent : IBaseComponent
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }
        public IDateTimeProvider DateTimeProvider { get; set; }
    }
}