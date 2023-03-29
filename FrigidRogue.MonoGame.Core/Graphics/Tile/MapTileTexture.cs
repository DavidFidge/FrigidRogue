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
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            SpriteFont spriteFont,
            char foregroundCharacter,
            Color foregroundColor,
            Color? backgroundColour = null,
            float spriteBatchDrawDepth = 0
        ) : base(graphicsDevice, tileWidth, tileHeight)
        {
            var previousRenderTargets = graphicsDevice.GetRenderTargets();

            var glyph = spriteFont.GetGlyphs()[foregroundCharacter];

            var offset = new Vector2((tileWidth - glyph.BoundsInTexture.Width) / 2, (tileHeight - glyph.Cropping.Height) / 2);
            
            graphicsDevice.SetRenderTarget(_renderTarget);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (backgroundColour != null)
                graphicsDevice.Clear(backgroundColour.Value);

            _spriteBatch.DrawString(spriteFont, foregroundCharacter.ToString(), offset, foregroundColor);

            _spriteBatch.End();

            graphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;
            
            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }
        
        /// <summary>
        /// Create a tile that only has a background, no foreground
        /// </summary>
        public MapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            Color backgroundColour,
            float spriteBatchDrawDepth = 0) : base(graphicsDevice, tileWidth, tileHeight)
        {
            var previousRenderTargets = graphicsDevice.GetRenderTargets();
            
            graphicsDevice.SetRenderTarget(_renderTarget);

            graphicsDevice.Clear(backgroundColour);

            graphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
            _opacity = (float)backgroundColour.A / byte.MaxValue;
        }

        /// <summary>
        /// Create a tile from a bitmap font character. The bitmap font must be a transparent background on a white foreground and must be a 16 character x 16 character image.
        /// </summary>
        public MapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            Texture2D bitmapFontTexture,
            char character,
            Color foregroundColor,
            Color? backgroundColour = null,
            float spriteBatchDrawDepth = 0
        ) : base(graphicsDevice, tileWidth, tileHeight)
        {
            var previousRenderTargets = graphicsDevice.GetRenderTargets();

            graphicsDevice.SetRenderTarget(_renderTarget);

            graphicsDevice.Clear(backgroundColour ?? Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            var bitmapCharacterWidth = bitmapFontTexture.Width / 16;
            var bitmapCharacterHeight = bitmapFontTexture.Height / 16;

            var characterIndex = (int)character;

            var characterRegion = new Rectangle((characterIndex % 16) * bitmapCharacterWidth, (characterIndex / 16) * bitmapCharacterHeight, bitmapCharacterWidth, bitmapCharacterHeight);

            _spriteBatch.Draw(bitmapFontTexture, new Rectangle(0, 0, characterRegion.Width, characterRegion.Height), characterRegion, foregroundColor);

            _spriteBatch.End();

            graphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }

        /// <summary>
        /// Create a tile from a Texture2D with foreground and background of texture
        /// </summary>
        public MapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            Texture2D texture,
            float spriteBatchDrawDepth = 0
        ) : base(graphicsDevice, tileWidth, tileHeight)
        {
            if (tileWidth == texture.Width && tileHeight == texture.Height)
            {
                _tileTexture = new Texture2D(graphicsDevice, tileWidth, tileHeight);

                var colours = new Color[tileWidth * tileHeight];

                texture.GetData(colours);
                _tileTexture.SetData(colours);
            }
            else
            {
                var previousRenderTargets = graphicsDevice.GetRenderTargets();

                graphicsDevice.SetRenderTarget(_renderTarget);

                graphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                _spriteBatch.Draw(texture, _renderTarget.Bounds, texture.Bounds, Color.White);

                _spriteBatch.End();

                graphicsDevice.SetRenderTargets(previousRenderTargets);

                _tileTexture = _renderTarget;
            }

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }

        /// <summary>
        /// Create a tile from a Texture2D with custom foreground and background
        /// </summary>
        public MapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            Texture2D texture,
            Color foregroundColor,
            Color? backgroundColour = null,
            float spriteBatchDrawDepth = 0
        ) : base(graphicsDevice, tileWidth, tileHeight)
        {
            var previousRenderTargets = graphicsDevice.GetRenderTargets();

            graphicsDevice.SetRenderTarget(_renderTarget);

            graphicsDevice.Clear(backgroundColour ?? Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            _spriteBatch.Draw(texture, _renderTarget.Bounds, texture.Bounds, foregroundColor);

            _spriteBatch.End();

            graphicsDevice.SetRenderTargets(previousRenderTargets);

            _tileTexture = _renderTarget;

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }
    }
}