using System.Collections.Generic;

using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public abstract class BaseMapQuad : IDrawable
    {
        private static Dictionary<string, Texture2D> _cachedTextures = new Dictionary<string, Texture2D>();

        protected readonly IGameProvider _gameProvider;
        protected RenderTarget2D _renderTarget;
        protected SpriteBatch _spriteBatch;

        public bool IsVisible { get; set; }

        public BaseMapQuad(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;

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
        }

        public abstract void Draw(Matrix view, Matrix projection, Matrix world);
    }
}