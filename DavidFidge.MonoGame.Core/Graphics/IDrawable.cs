using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Graphics
{
    public interface IDrawable
    {
        void Draw(Matrix view, Matrix projection, Matrix world);
    }
}