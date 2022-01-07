using System;
using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class GoalMapQuad : IDrawable
    {
        private static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        private readonly IGameProvider _gameProvider;
        private readonly SpriteFont _spriteFont;

        private readonly TexturedQuadTemplate _foreground;
        public string Text { get; set; }
        public bool IsVisible { get; set; }

        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

        /// <summary>
        /// Create a tile that has a character and an optional background
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="foregroundCharacter"></param>
        /// <param name="foregroundColor"></param>
        public GoalMapQuad(
            IGameProvider gameProvider,
            float tileWidth,
            float tileHeight,
            SpriteFont spriteFont,
            Effect textureMaterialEffect,
            Color foregroundColor
        )
        {
            _gameProvider = gameProvider;
            _spriteFont = spriteFont;

            _renderTarget = new RenderTarget2D(
                _gameProvider.Game.GraphicsDevice,
                64,
                116,
                false,
                _gameProvider.Game.GraphicsDevice.PresentationParameters.BackBufferFormat,
                _gameProvider.Game.GraphicsDevice.PresentationParameters.DepthStencilFormat,
                0,
                RenderTargetUsage.PreserveContents
            );

            _spriteBatch = new SpriteBatch(_gameProvider.Game.GraphicsDevice);

            var texturedQuad = new TexturedQuadTemplate(_gameProvider);
            texturedQuad.LoadContent(tileWidth, tileHeight, _renderTarget, textureMaterialEffect);
            texturedQuad.Colour = foregroundColor;

            _foreground = texturedQuad;
        }

        private void SetTextTexture()
        {
            if (!_cachedTextures.ContainsKey(Text))
            {
                var previousRenderTargets = _gameProvider.Game.GraphicsDevice.GetRenderTargets();

                _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

                _gameProvider.Game.GraphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);

                _spriteBatch.DrawString(_spriteFont, Text, Vector2.Zero, Color.White);

                _spriteBatch.End();

                _gameProvider.Game.GraphicsDevice.SetRenderTargets(previousRenderTargets);

                var textureData = new Color[_renderTarget.Width * _renderTarget.Height];

                _renderTarget.GetData(textureData);

                var texture = new Texture2D(
                    _gameProvider.Game.GraphicsDevice,
                    _renderTarget.Width,
                    _renderTarget.Height
                );

                texture.SetData(textureData);

                _cachedTextures.Add(Text, texture);
            }

            _foreground.Texture = _cachedTextures[Text];
        }

        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                SetTextTexture();
                _foreground?.Draw(view, projection, world);
            }
        }
    }
}