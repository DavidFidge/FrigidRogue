using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class ZoomViewRequest : IRequest
    {
        public int Difference { get; }

        public ZoomViewRequest(int difference)
        {
            Difference = difference;
        }
    }
}