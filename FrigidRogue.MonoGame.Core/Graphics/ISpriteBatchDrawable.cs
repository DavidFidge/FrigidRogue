using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface ISpriteBatchDrawable
    {
        public bool IsVisible { get; set; }
        void SpriteBatchDraw(SpriteBatch spriteBatch);
    }
}