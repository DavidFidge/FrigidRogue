using FrigidRogue.MonoGame.Core.Interfaces.Components;
using MediatR;

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