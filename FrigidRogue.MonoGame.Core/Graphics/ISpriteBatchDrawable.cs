using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public interface ISpriteBatchDrawable
    {
        public bool IsVisible { get; set; }

        // A single texture atlas is used for this draw, making it very quick
        void SpriteBatchDraw(SpriteBatch spriteBatch);

        // This one allows tiles to draw any additional overlays like health bars
        void OverlayDraw(SpriteBatch spriteBatch);
    }
}