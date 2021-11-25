using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics
{
    public interface ICamera
    {
        void Reset();
        Matrix View { get; }
        Matrix Projection { get; }
        void Update();
        void Initialise();
        Ray GetPointerRay(int x, int y, bool normalised = true);
    }
}