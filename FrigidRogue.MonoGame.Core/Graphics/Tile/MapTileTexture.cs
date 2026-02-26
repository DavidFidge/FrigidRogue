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
        /// Create a tile from a bitmap font character. The bitmap font must be a transparent background on a white foreground and must be a 16 character x 16 character image.
        /// Draws the glyph into one of four quadrants based on the provided direction.
        /// </summary>
        public MapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            Texture2D bitmapFontTexture,
            char character,
            SadRogue.Primitives.Direction direction,
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

            // Compute destination rectangle so the glyph is drawn into one quadrant:
            // top-left (UpLeft), top-right (UpRight), bottom-left (DownLeft), bottom-right (DownRight).
            var quadrantWidth = tileWidth / 2;
            var quadrantHeight = tileHeight / 2;

            // Desired size is the source glyph size; scale down to fit quadrant if necessary.
            var srcWidth = characterRegion.Width;
            var srcHeight = characterRegion.Height;

            float scale = Math.Min(1f, Math.Min((float)quadrantWidth / srcWidth, (float)quadrantHeight / srcHeight));
            var destWidth = Math.Max(1, (int)(srcWidth * scale));
            var destHeight = Math.Max(1, (int)(srcHeight * scale));

            int destX = (tileWidth - destWidth) / 2;
            int destY = (tileHeight - destHeight) / 2;

            // Map directions to quadrants. Default centers the glyph if direction is not one of the four diagonals.
            switch (direction.Type)
            {
                case SadRogue.Primitives.Direction.Types.UpLeft:
                    destX = (quadrantWidth - destWidth) / 2;
                    destY = (quadrantHeight - destHeight) / 2;
                    break;
                case SadRogue.Primitives.Direction.Types.UpRight:
                    destX = quadrantWidth + (quadrantWidth - destWidth) / 2;
                    destY = (quadrantHeight - destHeight) / 2;
                    break;
                case SadRogue.Primitives.Direction.Types.DownLeft:
                    destX = (quadrantWidth - destWidth) / 2;
                    destY = quadrantHeight + (quadrantHeight - destHeight) / 2;
                    break;
                case SadRogue.Primitives.Direction.Types.DownRight:
                    destX = quadrantWidth + (quadrantWidth - destWidth) / 2;
                    destY = quadrantHeight + (quadrantHeight - destHeight) / 2;
                    break;
                default:
                    // For other directions, fall back to center of the tile
                    destX = (tileWidth - destWidth) / 2;
                    destY = (tileHeight - destHeight) / 2;
                    break;
            }

            var destRect = new Rectangle(destX, destY, destWidth, destHeight);

            _spriteBatch.Draw(bitmapFontTexture, destRect, characterRegion, foregroundColor);

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