using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public abstract class BaseMapTileTexture : IMapTileTexture
    {
        public Texture2D Texture2D => _tileTexture;
        protected Texture2D _tileTexture;
        protected RenderTarget2D _renderTarget;
        protected SpriteBatch _spriteBatch;
        protected float _opacity = 1f;
        public float Opacity => _opacity;

        // Further away = 1.0, closest = 0.0. Since map quads are largely a single unchanged texture
        // then 
        protected float _spriteBatchDrawDepth;

        public BaseMapTileTexture(GraphicsDevice graphicsDevice, int tileWidth, int tileHeight)
        {
            _renderTarget = new RenderTarget2D(
                graphicsDevice,
                tileWidth,
                tileHeight,
                false,
                graphicsDevice.PresentationParameters.BackBufferFormat,
                graphicsDevice.PresentationParameters.DepthStencilFormat,
                0,
                RenderTargetUsage.PreserveContents
            );

            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public virtual void SpriteBatchDraw(SpriteBatch spriteBatch, Rectangle destinationRectangle, float? opacityOverride = null)
        {
            var drawColour = Color.White;

            if (opacityOverride is < 1f)
                drawColour.A = (byte)(opacityOverride * byte.MaxValue);
            else
                drawColour.A = (byte)(_opacity * byte.MaxValue);
            
            spriteBatch.Draw(_tileTexture, destinationRectangle, null, drawColour, 0f, Vector2.Zero, SpriteEffects.None, _spriteBatchDrawDepth);
        }
    }
}
