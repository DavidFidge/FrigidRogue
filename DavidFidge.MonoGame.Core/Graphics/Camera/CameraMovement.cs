using System;

namespace DavidFidge.MonoGame.Core.Graphics.Camera
{
    [Flags]
    public enum CameraMovement
    {
        None = 0,
        PanLeft = 1,
        PanRight = 2,
        PanUp = 4,
        PanDown = 8,
        RotateLeft = 16,
        RotateRight = 32,
        RotateUp = 64,
        RotateDown = 128,
        Forward = 256,
        Backward = 512,
    };
}