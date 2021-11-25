using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Graphics.Models
{
    public abstract class ModelDrawer : IModelDrawer
    {
        private Matrix[] _transforms;
        private Matrix[] _modelWorldTransforms;

        protected GameModel Model { get; set; }

        protected ModelDrawer()
        {
            _modelWorldTransforms = _transforms;
        }

        // To be used later for animation
        public void ChangeTransform(Matrix[] transforms)
        {
            _transforms = transforms;
        }

        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            if (_transforms == null)
                _transforms = Model.OriginalTransforms;

            // All the model bone transforms are relative to its parent bone.
            // First copy the relative transforms in this drawer instance to the model,
            // then get the absolute transforms from the model which are in world-space.
            _modelWorldTransforms = Model.GetAbsoluteTransforms(_transforms);

            foreach (var mesh in Model.Model.Meshes)
            {
                foreach (var effect in mesh.Effects)
                {
                    effect.SetWorldViewProjection(
                        _modelWorldTransforms[mesh.ParentBone.Index] * world,
                        view,
                        projection
                    );

                    foreach (var pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        mesh.Draw();
                    }
                }
            }
        }
    }
}