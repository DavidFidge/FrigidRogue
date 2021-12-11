using FrigidRogue.MonoGame.Core.Graphics.Camera;

using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class MoveViewRequest : IRequest
    {
        public CameraMovement CameraMovementFlags { get; }

        public MoveViewRequest(CameraMovement cameraMovementFlags)
        {
            CameraMovementFlags = cameraMovementFlags;
        }
    }
}