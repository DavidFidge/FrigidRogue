using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Quads
{
    public abstract class BaseQuadTemplate : IDrawable
    {
        private int[] _quadIndices;
        private VertexPositionTexture[] _quadVertices;
        private Vector2 _dimensions;

        protected IGameProvider _gameProvider;
        public Effect Effect { get; set; }

        public VertexBuffer VertexBuffer { get; private set; }
        public IndexBuffer IndexBuffer { get; private set; }

        public bool AlphaEnabled { get; set; }

        /// <summary>
        /// None means that quads drawn first can never appear 'on top' of quads drawn later
        /// </summary>
        public DepthStencilState OpaquePixelDepthStencilState { get; } = DepthStencilState.None;

        public bool IsVisible { get; set; } = true;

        public void Draw(Matrix view, Matrix projection, Matrix world)
        {
            var graphicsDevice = _gameProvider.Game.GraphicsDevice;

            graphicsDevice.Indices = IndexBuffer;
            graphicsDevice.SetVertexBuffer(VertexBuffer);

            if (Effect == null)
                return;

            Effect.SetWorldViewProjection(
                world,
                view,
                projection
            );

            SetEffectParameters();

            if (AlphaEnabled)
            {
                graphicsDevice.BlendState = BlendState.AlphaBlend;

                if (Effect is BasicEffect basicEffect)
                    basicEffect.Alpha = GetBasicEffectAlphaValue();
                else
                    Effect.Parameters["AlphaEnabled"].SetValue(true);

                DrawOpaquePixels(graphicsDevice);
                DrawTransparentPixels(graphicsDevice);
            }
            else
            {
                if (Effect is BasicEffect basicEffect)
                    basicEffect.Alpha = 0f;
                else
                    Effect.Parameters["AlphaEnabled"].SetValue(false);

                DrawQuad(graphicsDevice);
            }

            // Reset render states
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        protected virtual float GetBasicEffectAlphaValue()
        {
            return 1f;
        }

        protected virtual void SetEffectParameters()
        {
        }

        private void DrawOpaquePixels(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.DepthStencilState = OpaquePixelDepthStencilState;

            if (!(Effect is BasicEffect))
                Effect.Parameters["AlphaTestGreater"].SetValue(true);

            DrawQuad(graphicsDevice);
        }

        private void DrawTransparentPixels(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            if (!(Effect is BasicEffect))
                Effect.Parameters["AlphaTestGreater"].SetValue(false);

            DrawQuad(graphicsDevice);
        }

        private void DrawQuad(GraphicsDevice graphicsDevice)
        {
            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    2
                );
            }
        }

        protected BaseQuadTemplate(IGameProvider gameProvider)
        {
            AlphaEnabled = true;
            _gameProvider = gameProvider;
        }

        protected void LoadContent(float width, float height)
        {
            LoadContentInternal(width, height, Vector3.Zero);
        }

        protected void LoadContent(float width, float height, Vector3 displacement)
        {
            LoadContentInternal(width, height, displacement);
        }

        private void LoadContentInternal(float width, float height, Vector3 displacement)
        {
            _dimensions.X = width;
            _dimensions.Y = height;
            
            // Halve the width and height - this is used so that points are placed in a fashion that the object will be centred on world origin
            var halfWidth = _dimensions.X / 2.0f;
            var halfHeight = _dimensions.Y / 2.0f;

            var topLeft = new Vector3(-halfWidth, halfHeight, 0.0f);
            var topRight = new Vector3(halfWidth, halfHeight, 0.0f);
            var bottomLeft = new Vector3(-halfWidth, -halfHeight, 0.0f);
            var bottomRight = new Vector3(halfWidth, -halfHeight, 0.0f);

            //add in the displacement factor - this displaces the quad off-centre, so you can do some interesting rotations on a pivot
            topLeft = Vector3.Add(topLeft, displacement);
            topRight = Vector3.Add(topRight, displacement);
            bottomLeft = Vector3.Add(bottomLeft, displacement);
            bottomRight = Vector3.Add(bottomRight, displacement);

            // Initialize the texture coordinates.
            var textureTopLeft = new Vector2(0.0f, 0.0f);
            var textureTopRight = new Vector2(1.0f, 0.0f);
            var textureBottomLeft = new Vector2(0.0f, 1.0f);
            var textureBottomRight = new Vector2(1.0f, 1.0f);

            _quadVertices = new VertexPositionTexture[4];

            // Vertices for the front of the quad.
            _quadVertices[0] = new VertexPositionTexture(topLeft, textureTopLeft);
            _quadVertices[1] = new VertexPositionTexture(topRight, textureTopRight);
            _quadVertices[2] = new VertexPositionTexture(bottomLeft, textureBottomLeft);
            _quadVertices[3] = new VertexPositionTexture(bottomRight, textureBottomRight);

            VertexBuffer = new VertexBuffer(
                _gameProvider.Game.GraphicsDevice,
                VertexPositionTexture.VertexDeclaration,
                _quadVertices.Length,
                BufferUsage.WriteOnly
                );

            VertexBuffer.SetData(_quadVertices);

            _quadIndices = new [] { 0, 1, 2, 2, 1, 3 };

            IndexBuffer = new IndexBuffer(
                _gameProvider.Game.GraphicsDevice,
                IndexElementSize.ThirtyTwoBits,
                _quadIndices.Length,
                BufferUsage.WriteOnly
                );

            IndexBuffer.SetData(_quadIndices);
        }
    }
}
