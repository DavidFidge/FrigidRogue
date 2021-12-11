using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class RotateViewRequest : IRequest
    {
        public float XRotation { get; }
        public float ZRotation { get; }

        public RotateViewRequest(float xRotation, float zRotation)
        {
            XRotation = xRotation;
            ZRotation = zRotation;
        }
    }
}