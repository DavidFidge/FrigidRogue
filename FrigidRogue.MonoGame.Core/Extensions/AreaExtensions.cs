using SadRogue.Primitives;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class AreaExtensions
    {
        public static Point GetGeometricCentroiod(this Area area, AdjacencyRule adjacencyRule)
        {
            var perimeterPoints = area.PerimeterPositions(adjacencyRule).ToList();

            // Compute the geometric centroid of the perimeter points (average X and Y)
            var avgX = perimeterPoints.Average(p => p.X);
            var avgY = perimeterPoints.Average(p => p.Y);

            var point = new Point((int)avgX, (int)avgY);

            return point;
        }
    }
}
