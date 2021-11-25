using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface IDrawable
    {
        void Draw(Matrix view, Matrix projection, Matrix world);
    }
}