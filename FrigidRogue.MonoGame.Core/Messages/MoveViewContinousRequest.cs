using FrigidRogue.MonoGame.Core.Components.Mediator;
using FrigidRogue.MonoGame.Core.Graphics.Camera;

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