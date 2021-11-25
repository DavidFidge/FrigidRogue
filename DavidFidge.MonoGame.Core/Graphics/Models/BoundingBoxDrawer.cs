using DavidFidge.MonoGame.Core.Extensions;
using DavidFidge.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Graphics.Models
{
    public class BoundingBoxDrawer : IDrawable
    {
        private readonly BoundingBoxModel _boundingBoxModel;
        private readonly IGameProvider _gameProvider;

        public BoundingBoxDrawer(BoundingBoxModel boundingBoxModel, IGameProvider gameProvider)
        {
            _boundingBoxModel = boundingBoxModel;
            _gameProvider = gameProvider;
        }
        
        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            if (_boundingBoxModel.BoundingBoxVertexBuffer != null)
            {
                var graphicsDevice = _gameProvider.Game.GraphicsDevice;

                graphicsDevice.Indices = _boundingBoxModel.BoundingBoxIndexBuffer;
                graphicsDevice.SetVertexBuffer(_boundingBoxModel.BoundingBoxVertexBuffer);

                _boundingBoxModel.BoundingBoxEffect.SetWorldViewProjection(
                    world,
                    view,
                    projection
                );

                foreach (var pass in _boundingBoxModel.BoundingBoxEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    graphicsDevice.DrawIndexedPrimitives(
                        PrimitiveType.LineList,
                        0,
                        0,
                        12
                    );
                }
            }
        }
    }
}