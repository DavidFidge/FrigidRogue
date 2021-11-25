using System.Linq;

using DavidFidge.MonoGame.Core.ContentPipeline;
using DavidFidge.MonoGame.Core.Extensions;
using DavidFidge.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Graphics.Models
{
    public class GameModel
    {
        private readonly string _modelKey;
        protected readonly IGameProvider _gameProvider;

        public Model Model { get; private set; }
        public Matrix[] OriginalTransforms { get; private set; }
        public BoundingBox BoundingBox { get; private set; }
        public BoundingSphere BoundingSphere { get; private set; }

        public GameModel(string model, IGameProvider gameProvider)
        {
            _modelKey = model;
            _gameProvider = gameProvider;
        }

        public void LoadContent()
        {
            Model = _gameProvider.Game.Content.Load<Model>(_modelKey);

            InitialiseEffects(_gameProvider, Model);

            OriginalTransforms = new Matrix[Model.Bones.Count];

            Model.CopyBoneTransformsTo(OriginalTransforms);

            if (Model.Tag is TagObject tag)
            {
                BoundingBox = tag.BoundingBox;
                BoundingSphere = tag.BoundingSphere;
            }
            else
            {
                BoundingBox = new BoundingBox();
                BoundingSphere = new BoundingSphere();
            }
        }

        public Matrix[] GetAbsoluteTransforms(Matrix[] matrices)
        {
            Model.CopyBoneTransformsFrom(matrices);

            var absoluteTransforms = new Matrix[Model.Bones.Count];

            // All the model bone transforms are relative to its parent bone.  Create absolute transforms that shuffle up all values and are relative to the world (i.e. absolute values).  These can then by multiplied by world matrix.
            Model.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            return absoluteTransforms;
        }

        private void InitialiseEffects(IGameProvider gameProvider, Model xnaModel)
        {
            var effects = xnaModel.Meshes.SelectMany(m => m.Effects).ToList();

            foreach (var effect in effects)
            {
                if (effect is BasicEffect basicEffect)
                    basicEffect.CopyLightingFrom(gameProvider.Game.EffectCollection.MasterEffectTemplate);
            }
        }
    }
}