using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DavidFidge.MonoGame.Core.Extensions
{
    public static class VertexExtensions
    {
        public static VertexPositionNormalTexture[] GenerateNormalsForTriangleStrip(this VertexPositionNormalTexture[] vertices, int[] indices)
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = Vector3.Zero;
            }

            var swappedWinding = false;

            for (var i = 2; i < indices.Length; i++)
            {
                var firstVector = vertices[indices[i - 1]].Position - vertices[indices[i]].Position;

                var secondVector = vertices[indices[i - 2]].Position - vertices[indices[i]].Position;

                var normal = Vector3.Cross(firstVector, secondVector);

                normal.Normalize();

                if (swappedWinding)
                    normal *= -1;

                if (!float.IsNaN(normal.X))
                {
                    vertices[indices[i]].Normal += normal;
                    vertices[indices[i - 1]].Normal += normal;
                    vertices[indices[i - 2]].Normal += normal;
                }

                swappedWinding = !swappedWinding;
            }

            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal.Normalize();
            }

            return vertices;
        }
    }
}
