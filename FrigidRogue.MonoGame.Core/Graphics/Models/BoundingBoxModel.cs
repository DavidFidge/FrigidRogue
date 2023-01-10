using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Models
{
    public class BoundingBoxModel
    {
        private readonly BoundingBox _boundingBox;
        private readonly IGameProvider _gameProvider;
        public Effect BoundingBoxEffect { get; private set; }
        public VertexBuffer BoundingBoxVertexBuffer { get; private set; }
        public IndexBuffer BoundingBoxIndexBuffer { get; private set; }

        public IConfigurationSettings ConfigurationSettings { get; set; }

        public BoundingBoxModel(BoundingBox boundingBox, IGameProvider gameProvider)
        {
            _boundingBox = boundingBox;
            _gameProvider = gameProvider;

            SetupBoundingBoxEffect();
        }

        private void SetupBoundingBoxEffect()
        {
            if (ConfigurationSettings != null && ConfigurationSettings.GraphicsSettings.ShowBoundingBoxes)
            {
                var boundingBoxVertices = _boundingBox
                    .GetCorners()
                    .Select(c => new VertexPositionColor(c, Color.White))
                    .ToArray();

                var boundingBoxIndices = new int[24];

                var currentIndex = 0;

                boundingBoxIndices[currentIndex++] = 0;
                boundingBoxIndices[currentIndex++] = 1;

                boundingBoxIndices[currentIndex++] = 0;
                boundingBoxIndices[currentIndex++] = 3;

                boundingBoxIndices[currentIndex++] = 0;
                boundingBoxIndices[currentIndex++] = 4;

                boundingBoxIndices[currentIndex++] = 1;
                boundingBoxIndices[currentIndex++] = 2;

                boundingBoxIndices[currentIndex++] = 1;
                boundingBoxIndices[currentIndex++] = 5;

                boundingBoxIndices[currentIndex++] = 2;
                boundingBoxIndices[currentIndex++] = 3;

                boundingBoxIndices[currentIndex++] = 2;
                boundingBoxIndices[currentIndex++] = 6;

                boundingBoxIndices[currentIndex++] = 3;
                boundingBoxIndices[currentIndex++] = 7;

                boundingBoxIndices[currentIndex++] = 4;
                boundingBoxIndices[currentIndex++] = 5;

                boundingBoxIndices[currentIndex++] = 4;
                boundingBoxIndices[currentIndex++] = 7;

                boundingBoxIndices[currentIndex++] = 5;
                boundingBoxIndices[currentIndex++] = 6;

                boundingBoxIndices[currentIndex++] = 6;
                boundingBoxIndices[currentIndex++] = 7;

                BoundingBoxVertexBuffer = new VertexBuffer(
                    _gameProvider.Game.GraphicsDevice,
                    VertexPositionColor.VertexDeclaration,
                    boundingBoxVertices.Length,
                    BufferUsage.WriteOnly
                );

                BoundingBoxVertexBuffer.SetData(boundingBoxVertices);

                BoundingBoxIndexBuffer = new IndexBuffer(
                    _gameProvider.Game.GraphicsDevice,
                    IndexElementSize.ThirtyTwoBits,
                    boundingBoxIndices.Length,
                    BufferUsage.WriteOnly
                );

                BoundingBoxIndexBuffer.SetData(boundingBoxIndices);

                BoundingBoxEffect = _gameProvider.Game.EffectCollection.BuildMaterialEffect(Color.Yellow);
            }
        }
    }
}
