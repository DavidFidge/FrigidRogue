using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrigidRogue.MonoGame.Core.Graphics.Cylinder
{
    /// <summary>
    /// Creates a cylinder with no top or bottom caps and no inside faces
    /// </summary>
    public class CylinderGenerator
    {
        private readonly PrimitiveType _primitiveType;
        public int[] Indexes;
        public VertexPositionNormalTexture[] Vertices;
        public int CircleVertexCount { get; }
        public int HeightVertexCount { get; }
        public float Radius { get; }
        public float Height { get; }

        public CylinderGenerator(
            int circleVertexCount,
            int heightVertexCount,
            float radius,
            float height,
            PrimitiveType primitiveType
        )
        {
            _primitiveType = primitiveType;
            CircleVertexCount = circleVertexCount;
            HeightVertexCount = heightVertexCount;
            Radius = radius;
            Height = height;

            if (HeightVertexCount < 2)
            {
                throw new ArgumentException("Height Vertex Count must be at least 2 (for bottom of cylinder and top of cylinder)", nameof(heightVertexCount));
            }
        }

        public CylinderGenerator CreateGeometry()
        {
            Vertices = CreateVertices();

            if (_primitiveType == PrimitiveType.TriangleStrip)
                Indexes = CreateIndexesStrip();
            else
                Indexes = CreateIndexesList();

            return this;
        }

        public int PrimitiveCount
        {
            get
            {
                if (_primitiveType == PrimitiveType.TriangleStrip)
                    return Indexes.Length - 2;

                return (Indexes.Length * 2) / 3;
            }
        }

        private VertexPositionNormalTexture[] CreateVertices()
        {
            var vertices = new VertexPositionNormalTexture[CircleVertexCount * HeightVertexCount];

            var circleSeparation = (Math.PI * 2) / CircleVertexCount;
            var heightInterval = Height / (HeightVertexCount - 1);

            var i = 0;

            for (var cylinderHeight = 0; cylinderHeight < HeightVertexCount; cylinderHeight++)
            {
                for (var circleVertex = 0; circleVertex < CircleVertexCount; circleVertex++)
                {
                    var angle = circleSeparation * circleVertex;

                    var x = (float) Math.Sin(angle) * Radius;
                    var y = (float) Math.Cos(angle) * Radius;

                    var position = new Vector3(x, y, cylinderHeight * heightInterval);

                    var normal = new Vector3(x, y, 0);
                    var texture = new Vector2(x / (CircleVertexCount / 10f), y / (HeightVertexCount / 10f));

                    vertices[i++] = new VertexPositionNormalTexture(position, normal, texture);
                }
            }

            return vertices;
        }

        public int[] CreateIndexesStrip()
        {
            var terrainIndexes = new int[(CircleVertexCount + 1) * 2 * (HeightVertexCount - 1)];

            var i = 0;
            var y = 0;

            while (y < HeightVertexCount - 1)
            {
                // create triangle strip indexes going forwards
                for (var x = 0; x < CircleVertexCount; x++)
                {
                    terrainIndexes[i++] = x + (y + 1) * CircleVertexCount;
                    terrainIndexes[i++] = x + y * CircleVertexCount;
                }

                // Link up with vertices at start
                terrainIndexes[i++] = (y + 1) * CircleVertexCount;
                terrainIndexes[i++] = y * CircleVertexCount;

                // move up to next row and create triangle strip indexes going backwards
                y++;

                if (y < HeightVertexCount - 1)
                {
                    terrainIndexes[i++] = y * CircleVertexCount;
                    terrainIndexes[i++] = (y + 1) * CircleVertexCount;

                    for (var x = CircleVertexCount - 1; x >= 0; x--)
                    {
                        terrainIndexes[i++] = x + y * CircleVertexCount;
                        terrainIndexes[i++] = x + (y + 1) * CircleVertexCount;
                    }
                }

                y++;
            }

            return terrainIndexes;
        }

        public int[] CreateIndexesList()
        {
            var terrainIndexes = new int[CircleVertexCount * (HeightVertexCount - 1) * 6];

            var i = 0;

            for(var y = 0; y < HeightVertexCount - 1; y++)
            {
                // Create triangle strip indexes going forwards
                for (var x = 0; x < CircleVertexCount - 1; x++)
                {
                    terrainIndexes[i++] = x + (y + 1) * CircleVertexCount;
                    terrainIndexes[i++] = x + y * CircleVertexCount;
                    terrainIndexes[i++] = x + 1 + (y + 1) * CircleVertexCount;

                    terrainIndexes[i++] = x + 1 + (y + 1) * CircleVertexCount;
                    terrainIndexes[i++] = x + y * CircleVertexCount;
                    terrainIndexes[i++] = x + 1 + y * CircleVertexCount;
                }

                // Link up with vertices at start
                terrainIndexes[i++] = CircleVertexCount - 1 + (y + 1) * CircleVertexCount;
                terrainIndexes[i++] = CircleVertexCount - 1 + y * CircleVertexCount;
                terrainIndexes[i++] = (y + 1) * CircleVertexCount;

                terrainIndexes[i++] = (y + 1) * CircleVertexCount;
                terrainIndexes[i++] = CircleVertexCount - 1 + y * CircleVertexCount;
                terrainIndexes[i++] = y * CircleVertexCount;
            }

            return terrainIndexes;
        }
    }
}
