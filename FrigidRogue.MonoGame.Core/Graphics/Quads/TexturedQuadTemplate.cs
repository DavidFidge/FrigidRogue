using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class TexturedQuadTemplate : BaseQuadTemplate
    {
        public TexturedQuadTemplate(IGameProvider gameProvider)
           : base(gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public void LoadContent(float width, float height, Texture2D texture)
        {
            LoadContent(width, height);

            Effect = _gameProvider.Game.EffectCollection.BuildMaterialTextureEffect(texture);
        }
        
        public void LoadContent(float width, float height, Texture2D texture, Effect effect)
        {
            LoadContent(width, height);

            Effect = effect;

            Effect.Parameters["Texture"].SetValue(texture);
        }

        public void LoadContent(Vector2 size, Texture2D texture)
        {
            LoadContent(size.X, size.Y, texture);
        }

        public void SetTexture(Texture2D texture)
        {
            if (Effect is BasicEffect basicEffect)
                basicEffect.Texture = texture;
        }
    }
}