using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public abstract class BaseMapTileTexture
    {
        private static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        protected readonly IGameProvider _gameProvider;
        protected RenderTarget2D _renderTarget;
        protected SpriteBatch _spriteBatch;
        protected Texture2D _tileTexture;

        // Further away = 1.0, closest = 0.0. Since map quads are largely a single unchanged texture
        // then 
        protected float _spriteBatchDrawDepth;

        public bool IsVisible { get; set; }
        
        public BaseMapTileTexture(IGameProvider gameProvider, int tileWidth, int tileHeight)
        {
            _gameProvider = gameProvider;

            _renderTarget = new RenderTarget2D(
                _gameProvider.Game.GraphicsDevice,
                tileWidth,
                tileHeight,
                false,
                _gameProvider.Game.GraphicsDevice.PresentationParameters.BackBufferFormat,
                _gameProvider.Game.GraphicsDevice.PresentationParameters.DepthStencilFormat,
                0,
                RenderTargetUsage.PreserveContents
            );

            _spriteBatch = new SpriteBatch(_gameProvider.Game.GraphicsDevice);
        }

        public virtual void SpriteBatchDraw(SpriteBatch spriteBatch, Rectangle destinationRectangle, float? opacity = null)
        {
            var drawColour = Color.White;

            if (opacity is < 1f)
                drawColour.A = (byte)(opacity * 255);
            
            spriteBatch.Draw(_tileTexture, destinationRectangle, null, drawColour, 0f, Vector2.Zero, SpriteEffects.None, _spriteBatchDrawDepth);
        }
    }
}