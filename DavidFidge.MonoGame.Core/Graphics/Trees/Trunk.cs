using DavidFidge.MonoGame.Core.Graphics.Cylinder;
using DavidFidge.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Graphics.Trees
{
    public class Trunk
    {
        private readonly IGameProvider _gameProvider;
        private readonly string _textureName;

        public int TrunkCircumferenceVertexCount { get; set; }
        public int TrunkHeightVertexCount { get; set; }
        public float Radius { get; set; }
        public float Height { get; set; }

        public Trunk(IGameProvider gameProvider, string textureName)
        {
            _gameProvider = gameProvider;
            _textureName = textureName;
        }

        public ModelMeshPart CreateModelMeshPart(IGameProvider gameProvider)
        {   
            var cylinder = new CylinderGenerator(
                    TrunkCircumferenceVertexCount,
                    TrunkHeightVertexCount,
                    Radius,
                    Height,
                    PrimitiveType.TriangleList)
                .CreateGeometry();

            var vertexBuffer = new VertexBuffer(gameProvider.Game.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, cylinder.Vertices.Length, BufferUsage.WriteOnly
            );

            vertexBuffer.SetData(cylinder.Vertices);

            var indexBuffer = new IndexBuffer(
                gameProvider.Game.GraphicsDevice,
                IndexElementSize.ThirtyTwoBits,
                cylinder.Indexes.Length,
                BufferUsage.WriteOnly
            );

            indexBuffer.SetData(cylinder.Indexes);

            var modelMeshPart = new ModelMeshPart
            {
                IndexBuffer = indexBuffer,
                VertexBuffer = vertexBuffer,
                NumVertices = vertexBuffer.VertexCount,
                PrimitiveCount = cylinder.PrimitiveCount
            };

            return modelMeshPart;
        }

        public void CreateTrunkEffect(ModelMeshPart modelMeshPart)
        {
            var buildTextureEffect = _gameProvider.Game.EffectCollection.BuildTextureEffect(_textureName);

            modelMeshPart.Effect = buildTextureEffect;
        }
    }
}