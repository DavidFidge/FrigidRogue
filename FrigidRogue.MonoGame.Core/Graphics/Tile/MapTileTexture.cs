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

        /// <summary>
        /// Create a tile from a bitmap font character. The bitmap font must be a transparent background on a white foreground.
        /// </summary>
        public MapTileTexture(
            IGameProvider gameProvider,
            int tileWidth,
            int tileHeight,
            Texture2D bitmapFontTexture,
            char character,
            float spriteBatchDrawDepth,
            Color foregroundColor,
            Color? backgroundColour = null
        ) : base(gameProvider, tileWidth, tileHeight)
        {
            var previousRenderTargets = _gameProvider.Game.GraphicsDevice.GetRenderTargets();

            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _gameProvider.Game.GraphicsDevice.Clear(backgroundColour ?? Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            var bitmapCharacterDimensions = bitmapFontTexture.Width / 16;

            var characterIndex = (int)character;

            var characterRegion = new Rectangle((characterIndex % 16) * bitmapCharacterDimensions, (characterIndex / 16) * bitmapCharacterDimensions, bitmapCharacterDimensions, bitmapCharacterDimensions);

            _spriteBatch.Draw(bitmapFontTexture, new Rectangle(0, 0, characterRegion.Width, characterRegion.Height), characterRegion, foregroundColor);

            _spriteBatch.End();

            _gameProvider.Game.GraphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }
    }
}