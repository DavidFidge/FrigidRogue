using FrigidRogue.MonoGame.Core.Interfaces.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class TexturedQuadTemplate : BaseQuadTemplate
    {
        public Texture2D Texture { get; set; }
        public Color Colour { get; set; } = Color.White;

        public TexturedQuadTemplate(IGameProvider gameProvider)
           : base(gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public void LoadContent(float width, float height, Texture2D texture)
        {
            Texture = texture;

            LoadContent(width, height);

            Effect = _gameProvider.Game.EffectCollection.BuildMaterialTextureEffect(texture);
        }
        
        public void LoadContent(float width, float height, Texture2D texture, Effect effect)
        {
            LoadContent(width, height);

            Effect = effect;
            Texture = texture;
        }

        public void LoadContent(Vector2 size, Texture2D texture)
        {
            LoadContent(size.X, size.Y, texture);
        }

        protected override void SetEffectParameters()
        {
            if (Effect is BasicEffect basicEffect)
            {
                basicEffect.Texture = Texture;
                basicEffect.DiffuseColor = Colour.ToVector3();
            }
            else
            {
                Effect.Parameters["Texture"].SetValue(Texture);
                Effect.Parameters["Colour"].SetValue(Colour.ToVector4());
            }
        }

        protected override float GetBasicEffectAlphaValue()
        {
            return Colour.A / 256f;
        }

        public override void Dispose()
        {
            base.Dispose();
            Texture?.Dispose();
        }
    }
}