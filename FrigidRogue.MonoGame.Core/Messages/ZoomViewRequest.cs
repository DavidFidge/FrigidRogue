
using FrigidRogue.MonoGame.Core.Components.Mediator;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class ZoomViewRequest : IRequest
    {
        public float Difference { get; }

        public ZoomViewRequest(float difference)
        {
            Difference = difference;
        }
    }
}