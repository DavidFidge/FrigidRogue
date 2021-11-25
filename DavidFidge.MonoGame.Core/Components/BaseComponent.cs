using MediatR;

using Serilog;

namespace DavidFidge.MonoGame.Core.Components
{
    public abstract class BaseComponent
    {
        public IMediator Mediator { get; set; }
        public ILogger Logger { get; set; }
    }
}