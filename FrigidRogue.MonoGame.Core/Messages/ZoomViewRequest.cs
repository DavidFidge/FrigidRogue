using MediatR;

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