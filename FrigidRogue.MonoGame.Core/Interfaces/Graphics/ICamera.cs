using FrigidRogue.MonoGame.Core.Services;
using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics
{
    public interface ICamera
    {
        void Reset();
        Matrix View { get; }
        Matrix Projection { get; }
        float MoveSensitivity { get; set; }
        float ZoomSensitivity { get; set; }
        float RotateSensitivity { get; set; }
        RenderResolution RenderResolution { get; set; }
        void Update();
        void Initialise();
        Ray GetPointerRay(int x, int y, bool normalised = true);
    }
}