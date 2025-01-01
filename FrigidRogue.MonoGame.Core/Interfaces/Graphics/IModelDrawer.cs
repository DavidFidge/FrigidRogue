using Microsoft.Xna.Framework;
using IDrawable = FrigidRogue.MonoGame.Core.Graphics.IDrawable;

namespace FrigidRogue.MonoGame.Core.Interfaces.Graphics
{
    public interface IModelDrawer : IDrawable
    {
        void ChangeTransform(Matrix[] transforms);
    }
}