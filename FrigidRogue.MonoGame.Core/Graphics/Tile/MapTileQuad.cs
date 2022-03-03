using System;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class MapTileQuad : BaseMapQuad
    {
        private readonly TexturedQuadTemplate _foreground;
        private readonly MaterialQuadTemplate _background;

        /// <summary>
        /// Create a tile that has a character and an optional background
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="foregroundCharacter"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColour"></param>
        public MapTileQuad(
            IGameProvider gameProvider,
            float tileWidth,
            float tileHeight,
            SpriteFont spriteFont,
            Effect textureMaterialEffect,
            char foregroundCharacter,
            Color foregroundColor,
            Color? backgroundColour = null
        ) : base(gameProvider)
        {

            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);

            _spriteBatch.DrawString(spriteFont, foregroundCharacter.ToString(), Vector2.Zero, Color.White);

            _spriteBatch.End();

            _gameProvider.Game.GraphicsDevice.SetRenderTarget(null);

            var texturedQuad = new TexturedQuadTemplate(_gameProvider);
            texturedQuad.LoadContent(tileWidth, tileHeight, _renderTarget, textureMaterialEffect);
            texturedQuad.Colour = foregroundColor;

            _foreground = texturedQuad;

            if (backgroundColour != null)
                _background = CreateBackgroundQuad(tileWidth, tileHeight, backgroundColour.Value);
        }

        /// <summary>
        /// Create a tile that only has a background, no foreground
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="backgroundColour"></param>
        public MapTileQuad(IGameProvider gameProvider, float tileWidth, float tileHeight, Color backgroundColour) : base(gameProvider)
        {
            _background = CreateBackgroundQuad(tileWidth, tileHeight, backgroundColour);
        }

        private MaterialQuadTemplate CreateBackgroundQuad(float tileWidth, float tileHeight, Color backgroundColour)
        {
            var background = new MaterialQuadTemplate(_gameProvider);
            background.AlphaEnabled = true;
            background.LoadContent(tileWidth, tileHeight, backgroundColour);

            return background;
        }

        public override void Draw(Matrix view, Matrix projection, Matrix world)
        {
            _background?.Draw(view, projection, world);
            _foreground?.Draw(view, projection, world);
        }

        public void Draw(Matrix view, Matrix projection, Matrix world, float backgroundOpacity)
        {
            var oldColour = _background.Colour;
            _background.SetColourOpacity(backgroundOpacity);
            _background?.Draw(view, projection, world);
            _background.Colour = oldColour;

            _foreground?.Draw(view, projection, world);
        }
    }
}