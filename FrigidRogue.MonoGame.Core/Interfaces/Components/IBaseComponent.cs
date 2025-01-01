using FrigidRogue.MonoGame.Core.Components.Mediator;
using Serilog;

namespace FrigidRogue.MonoGame.Core.Components
{
    public interface IBaseComponent
    {
        IMediator Mediator { get; set; }
        ILogger Logger { get; set; }
    }
}