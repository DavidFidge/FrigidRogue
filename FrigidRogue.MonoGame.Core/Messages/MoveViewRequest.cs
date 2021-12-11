using FrigidRogue.MonoGame.Core.Graphics.Camera;

using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class MoveViewRequest : IRequest
    {
        public CameraMovementType CameraMovementTypeFlags { get; }

        public MoveViewRequest(CameraMovementType cameraMovementTypeFlags)
        {
            CameraMovementTypeFlags = cameraMovementTypeFlags;
        }
    }
}