using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Components;
using FrigidRogue.MonoGame.Core.Interfaces.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Cylinder
{
    public class Cylinder : IDrawable
    {
        private readonly IGameProvider _gameProvider;
        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private Effect _effect;
        private int _primitiveCount;

        public Cylinder(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public void LoadContent(float radius, float height)
        {
            var cylinderGenerator = new CylinderGenerator(
                10,
                2,
                radius,
                height,
                PrimitiveType.TriangleStrip
            );

            cylinderGenerator.CreateGeometry();
            _primitiveCount = cylinderGenerator.PrimitiveCount;

            var vertices = cylinderGenerator.Vertices;
            var indexes = cylinderGenerator.Indexes;

            vertices.GenerateNormalsForTriangleStrip(indexes);

            _vertexBuffer = new VertexBuffer(
                _gameProvider.Game.GraphicsDevice,
                VertexPositionNormalTexture.VertexDeclaration,
                vertices.Length,
                BufferUsage.WriteOnly
            );

            _vertexBuffer.SetData(vertices);

            _indexBuffer = new IndexBuffer(
                _gameProvider.Game.GraphicsDevice,
                IndexElementSize.ThirtyTwoBits,
                indexes.Length,
                BufferUsage.WriteOnly
            );

            _indexBuffer.SetData(indexes);

            _effect = _gameProvider.Game.EffectCollection.BuildMaterialEffect(Color.Blue);
        }

        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            var graphicsDevice = _gameProvider.Game.GraphicsDevice;

            graphicsDevice.Indices = _indexBuffer;
            graphicsDevice.SetVertexBuffer(_vertexBuffer);

            if (_effect == null)
                return;
            
            _effect.SetWorldViewProjection(
                world,
                view,
                projection
            );

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleStrip,
                    0,
                    0,
                    _primitiveCount
                );
            }
        }
    }
}
