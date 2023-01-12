using FrigidRogue.MonoGame.Core.Graphics.Camera;

using MediatR;

namespace FrigidRogue.MonoGame.Core.Messages
{
    public class MoveViewContinousRequest : IRequest
    {
        public CameraMovementType CameraMovementTypeFlags { get; }

        public MoveViewContinousRequest(CameraMovementType cameraMovementTypeFlags)
        {
            CameraMovementTypeFlags = cameraMovementTypeFlags;
        }
    }
}