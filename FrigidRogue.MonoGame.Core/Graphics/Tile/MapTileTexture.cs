using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class MapTileTexture : BaseMapTileTexture
    {
        /// <summary>
        /// Create a tile that has a character and an optional background
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="foregroundCharacter"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColour"></param>
        public MapTileTexture(
            IGameProvider gameProvider,
            int tileWidth,
            int tileHeight,
            SpriteFont spriteFont,
            char foregroundCharacter,
            float spriteBatchDrawDepth,
            Color foregroundColor,
            Color? backgroundColour = null
        ) : base(gameProvider, tileWidth, tileHeight)
        {
            var previousRenderTargets = _gameProvider.Game.GraphicsDevice.GetRenderTargets();

            var glyph = spriteFont.GetGlyphs()[foregroundCharacter];

            var offset = new Vector2((tileWidth - glyph.BoundsInTexture.Width) / 2, (tileHeight - glyph.Cropping.Height) / 2);
            
            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (backgroundColour != null)
                _gameProvider.Game.GraphicsDevice.Clear(backgroundColour.Value);

            _spriteBatch.DrawString(spriteFont, foregroundCharacter.ToString(), offset, foregroundColor);

            _spriteBatch.End();

            _gameProvider.Game.GraphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;
            
            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }

        /// <summary>
        /// Create a tile that only has a background, no foreground
        /// </summary>
        public MapTileTexture(IGameProvider gameProvider, int tileWidth, int tileHeight, Color backgroundColour, float spriteBatchDrawDepth) : base(gameProvider, tileWidth, tileHeight)
        {
            var previousRenderTargets = _gameProvider.Game.GraphicsDevice.GetRenderTargets();
            
            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _gameProvider.Game.GraphicsDevice.Clear(backgroundColour);

            _gameProvider.Game.GraphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
            _opacity = (float)backgroundColour.A / byte.MaxValue;
        }
    }
}