using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface IDrawable
    {
        public bool IsVisible { get; set; }
        void Draw(Matrix view, Matrix projection, Matrix world);
    }
}