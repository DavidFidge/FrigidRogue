using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Extensions
{
    public static class BasicEffectExtensions
    {
        public static void CopyFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.CopyLightingFrom(source);
            basicEffect.CopyMaterialFrom(source);
            basicEffect.CopyFogFrom(source);
            basicEffect.CopyTransformsFrom(source);
            basicEffect.CopyTextureFrom(source);
        }

        public static void CopyFogFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.FogColor = source.FogColor;
            basicEffect.FogEnabled = source.FogEnabled;
            basicEffect.FogEnd = source.FogEnd;
            basicEffect.FogStart = source.FogStart;
        }

        public static void CopyTransformsFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.Projection = source.Projection;
            basicEffect.World = source.World;
            basicEffect.View = source.View;
        }

        public static void CopyTextureFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.Texture = source.Texture;
            basicEffect.TextureEnabled = source.TextureEnabled;
        }

        public static void CopyLightingFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.DirectionalLight0.CopyFrom(source.DirectionalLight0);
            basicEffect.DirectionalLight1.CopyFrom(source.DirectionalLight1);
            basicEffect.DirectionalLight2.CopyFrom(source.DirectionalLight2);

            basicEffect.AmbientLightColor = source.AmbientLightColor;
            basicEffect.LightingEnabled = source.LightingEnabled;
            basicEffect.PreferPerPixelLighting = source.PreferPerPixelLighting;
        }

        public static void CopyMaterialFrom(this BasicEffect basicEffect, BasicEffect source)
        {
            basicEffect.DiffuseColor = source.DiffuseColor;
            basicEffect.EmissiveColor = source.EmissiveColor;
            basicEffect.SpecularColor = source.SpecularColor;
            basicEffect.SpecularPower = source.SpecularPower;
            basicEffect.Alpha = source.Alpha;
        }

        private static void CopyFrom(this DirectionalLight directionalLight, DirectionalLight source)
        {
            directionalLight.Enabled = source.Enabled;
            directionalLight.DiffuseColor = source.DiffuseColor;
            directionalLight.SpecularColor = source.SpecularColor;
            directionalLight.Direction = source.Direction;
        }
    }
}
