using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class GoalMapTileTexture : BaseMapTileTexture
    {
        private static Dictionary<string, Texture2D> _cachedTextures = new();

        private readonly SpriteFont _spriteFont;
        private readonly Color _foregroundColor;
        public string Text { get; set; }

        public GoalMapTileTexture(
            IGameProvider gameProvider,
            int tileWidth,
            int tileHeight,
            SpriteFont spriteFont,
            Color foregroundColor
        ) : base(gameProvider, tileWidth, tileHeight)
        {
            _spriteFont = spriteFont;
            _foregroundColor = foregroundColor;
        }

        private void SetTextTexture()
        {
            if (String.IsNullOrEmpty(Text))
                throw new Exception("Text must be set before calling SetTextTexture");

            if (!_cachedTextures.ContainsKey(Text))
            {
                var previousRenderTargets = _gameProvider.Game.GraphicsDevice.GetRenderTargets();

                _gameProvider.Game.GraphicsDevice.SetRenderTarget(_renderTarget);

                _gameProvider.Game.GraphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                _spriteBatch.DrawString(_spriteFont, Text, Vector2.Zero, _foregroundColor);

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

            _tileTexture = _cachedTextures[Text];
        }

        public override void SpriteBatchDraw(SpriteBatch spriteBatch, Rectangle destinationRectangle, float? opacityOverride = null)
        {
            if (_tileTexture == null)
                throw new Exception("SetTextTexture must be called before drawing");

            if (!String.IsNullOrEmpty(Text))
            {
                SetTextTexture();
                base.SpriteBatchDraw(spriteBatch, destinationRectangle, opacityOverride);
            }
        }
    }
}