using DavidFidge.MonoGame.Core.Interfaces.Graphics;

namespace DavidFidge.MonoGame.Core.Graphics.Camera
{
    public interface IGameCamera : ICamera
    {
        CameraMovement GameUpdateContinuousMovement { get; set; }
        void Move(CameraMovement cameraMovement, float moveMagnitude);
        void Rotate(CameraMovement cameraMovement, float rotateMagnitude);
        void Zoom(int magnitude);
    }
}