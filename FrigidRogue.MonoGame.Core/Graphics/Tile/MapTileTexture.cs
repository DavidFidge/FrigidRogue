﻿using FrigidRogue.MonoGame.Core.Interfaces.Components;

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
            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (backgroundColour != null)
            {
                var textureColour = new Color[_renderTarget.Width * _renderTarget.Height];

                for (var i = 0; i < textureColour.Length; i++)
                {
                    textureColour[i] = backgroundColour.Value;
                }
                
                var texture = new Texture2D(
                    _gameProvider.Game.GraphicsDevice,
                    _renderTarget.Width,
                    _renderTarget.Height
                );
                
                texture.SetData(textureColour);
                
                _spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            }

            _spriteBatch.DrawString(spriteFont, foregroundCharacter.ToString(), Vector2.Zero, foregroundColor);

            _spriteBatch.End();

            _gameProvider.Game.GraphicsDevice.SetRenderTarget(null);

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }

        /// <summary>
        /// Create a tile that only has a background, no foreground
        /// </summary>
        public MapTileTexture(IGameProvider gameProvider, int tileWidth, int tileHeight, Color backgroundColour, float spriteBatchDrawDepth) : base(gameProvider, tileWidth, tileHeight)
        {
            _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            var textureColour = new Color[_renderTarget.Width * _renderTarget.Height];

            for (var i = 0; i < textureColour.Length; i++)
            {
                textureColour[i] = backgroundColour;
            }
            
            var texture = new Texture2D(
                _gameProvider.Game.GraphicsDevice,
                _renderTarget.Width,
                _renderTarget.Height
            );
            
            texture.SetData(textureColour);
            
            _spriteBatch.Draw(texture, Vector2.Zero, Color.White);

            _spriteBatch.End();

            _gameProvider.Game.GraphicsDevice.SetRenderTarget(null);

            _spriteBatchDrawDepth = spriteBatchDrawDepth;
        }
    }
}