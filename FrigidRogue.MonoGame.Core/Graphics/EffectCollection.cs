using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics
{
    public class EffectCollection : Dictionary<string, Effect>
    {
        private readonly IGameProvider _gameProvider;

        public BasicEffect MasterEffectTemplate { get; set; }
        public BasicEffect TextureEffectTemplate { get; set; }

        public EffectCollection(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public void Initialize()
        {
            TextureEffectTemplate = new BasicEffect(_gameProvider.Game.GraphicsDevice)
            {
                TextureEnabled = true
            };

            MasterEffectTemplate = new BasicEffect(_gameProvider.Game.GraphicsDevice);

            MasterEffectTemplate.EnableDefaultLighting();
            MasterEffectTemplate.DirectionalLight0.Direction = new Vector3(1, 1, 0);
            MasterEffectTemplate.DirectionalLight0.Enabled = true;
            MasterEffectTemplate.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            MasterEffectTemplate.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
            MasterEffectTemplate.DirectionalLight1.Enabled = false;
            MasterEffectTemplate.DirectionalLight2.Enabled = false;
            MasterEffectTemplate.SpecularColor = Vector3.Zero;
        }

        public BasicEffect BuildTextureEffect(string texture)
        {
            return BuildTextureEffect(_gameProvider.Game.Content.Load<Texture2D>(texture));
        }

        public BasicEffect BuildTextureEffect(Texture2D texture)
        {
            var basicEffect = (BasicEffect)MasterEffectTemplate.Clone();

            basicEffect.CopyTextureFrom(TextureEffectTemplate);

            basicEffect.Texture = texture;

            return basicEffect;
        }

        public BasicEffect BuildMaterialTextureEffect(string texture)
        {
            return BuildMaterialTextureEffect(_gameProvider.Game.Content.Load<Texture2D>(texture));
        }

        public BasicEffect BuildMaterialTextureEffect(Texture2D texture)
        {
            var basicEffect = (BasicEffect)MasterEffectTemplate.Clone();

            basicEffect.CopyTextureFrom(TextureEffectTemplate);

            basicEffect.Texture = texture;
            basicEffect.DiffuseColor = Color.White.ToVector3();
            basicEffect.LightingEnabled = false;

            return basicEffect;
        }

        public BasicEffect BuildMaterialEffect(Color colour)
        {
            var basicEffect = (BasicEffect)MasterEffectTemplate.Clone();

            basicEffect.DiffuseColor = colour.ToVector3();
            basicEffect.Alpha = colour.A / 255.0f;
            basicEffect.LightingEnabled = false;

            return basicEffect;
        }
    }
}
