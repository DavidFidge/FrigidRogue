using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class GoalMapTileTexture : BaseMapTileTexture
    {
        private static Dictionary<string, Texture2D> _cachedTextures = new();

        private readonly SpriteFont _spriteFont;
        private readonly Color _foregroundColor;
        private GraphicsDevice _graphicsDevice;
        public string Text { get; set; }

        public GoalMapTileTexture(
            GraphicsDevice graphicsDevice,
            int tileWidth,
            int tileHeight,
            SpriteFont spriteFont,
            Color foregroundColor
        ) : base(graphicsDevice, tileWidth, tileHeight)
        {
            _graphicsDevice = graphicsDevice;
            _spriteFont = spriteFont;
            _foregroundColor = foregroundColor;
        }

        private void SetTextTexture()
        {
            if (String.IsNullOrEmpty(Text))
                throw new Exception("Text must be set before calling SetTextTexture");

            if (!_cachedTextures.ContainsKey(Text))
            {
                var previousRenderTargets = _graphicsDevice.GetRenderTargets();

                _graphicsDevice.SetRenderTarget(_renderTarget);

                _graphicsDevice.Clear(Color.Transparent);

                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                _spriteBatch.DrawString(_spriteFont, Text, Vector2.Zero, _foregroundColor);

                _spriteBatch.End();

                _graphicsDevice.SetRenderTargets(previousRenderTargets);

                var textureData = new Color[_renderTarget.Width * _renderTarget.Height];

                _renderTarget.GetData(textureData);

                var texture = new Texture2D(
                    _graphicsDevice,
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
            if (!String.IsNullOrEmpty(Text))
            {
                SetTextTexture();
                base.SpriteBatchDraw(spriteBatch, destinationRectangle, opacityOverride);
            }
        }
    }
}