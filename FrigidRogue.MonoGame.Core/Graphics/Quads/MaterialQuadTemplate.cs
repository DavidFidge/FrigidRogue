using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public class MaterialQuadTemplate : BaseQuadTemplate
    {
        public Color Colour { get; set; }

        public MaterialQuadTemplate(IGameProvider gameProvider) : base(gameProvider)
        {
        }

        public void LoadContent(float width, float height, Color colour)
        {
            Colour = colour;

            LoadContent(width, height, colour, Vector3.Zero);
        }

        public void LoadContent(Vector2 size, Color colour)
        {
            LoadContent(size.X, size.Y, colour);
        }

        public void LoadContent(
            float width,
            float height,
            Color colour,
            Vector3 displacement)
        {
            Colour = colour;

            LoadContent(width, height, displacement);
            Effect = _gameProvider.Game.EffectCollection.BuildMaterialEffect(colour);
        }

        public void LoadContent(
            Vector2 size,
            Color colour,
            Vector3 displacement)
        {
            Colour = colour;
            LoadContent(size.X, size.Y, colour, displacement);
        }

        protected override void SetEffectParameters()
        {
            if (Effect is BasicEffect basicEffect)
            {
                basicEffect.DiffuseColor = Colour.ToVector3();
            }
            else
            {
                Effect.Parameters["Colour"].SetValue(Colour.ToVector4());
            }
        }

        protected override float GetBasicEffectAlphaValue()
        {
            return Colour.A / 256f;
        }
    }
}