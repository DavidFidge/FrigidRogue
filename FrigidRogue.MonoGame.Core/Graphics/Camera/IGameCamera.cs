using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Camera
{
    public interface IGameCamera : ICamera
    {
        CameraMovementType ContinuousCameraMovementType { get; set; }
        void Move(CameraMovementType cameraMovementType, float moveMagnitude);
        void Rotate(CameraMovementType cameraMovementType, float rotateMagnitude);
        void Zoom(float magnitude);
    }
}