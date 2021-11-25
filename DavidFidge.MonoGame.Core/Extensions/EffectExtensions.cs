using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Extensions
{
    public static class EffectExtensions
    {
        public static void SetWorldViewProjection(
            this Effect effect,
            Matrix world,
            Matrix view,
            Matrix projection
            )
        {
            if (effect is BasicEffect basicEffect)
            {
                basicEffect.World = world;
                basicEffect.View = view;
                basicEffect.Projection = projection;
            }
            else
            {
                effect.Parameters["World"].SetValue(world);
                effect.Parameters["View"].SetValue(view);
                effect.Parameters["Projection"].SetValue(projection);
            }
        }
    }
}
