using GoRogue.GameFramework;
using GoRogue.Random;

using SadRogue.Primitives;
using ShaiRandom.Generators;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class MapExtensions
    {
        public static Point RandomPositionAwayFrom(this Map map, Point pointAwayFrom, uint minDistance, Func<Point, IEnumerable<IGameObject>, bool> selector)
        {
            return GlobalRandom.DefaultRNG.RandomPosition(map, 
                (p, gameObjects) =>
                    MinSeparationFrom(pointAwayFrom, p, minDistance) &&
                    selector(p, gameObjects)
            );
        }

        public static Rectangle RectangleForRadiusAndPoint(this Map map, uint radius, Point point)
        {
            var xMin = (int)Math.Max(point.X - radius, 0);
            var yMin = (int)Math.Max(point.Y - radius, 0);
            var xMax = (int)Math.Min(point.X + radius, map.Width - 1);
            var yMax = (int)Math.Min(point.Y + radius, map.Height - 1);

            var rectangle = new Rectangle(
                new Point(xMin, yMin),
                new Point(xMax, yMax)
            );

            return rectangle;
        }
        
        public static Rectangle RectangleCoveringPoints(this Map map, uint padding, params Point[] points)
        {
            if (points.Length == 0)
            {
                return new Rectangle(
                    new Point(0, 0),
                    new Point(map.Width - 1, map.Height - 1)
                );
            }

            var minExtentX = points[0].X;
            var maxExtentX = minExtentX;
            var minExtentY = points[0].Y;
            var maxExtentY = minExtentY;
            
            foreach (var point in points.Skip(1))
            {
                minExtentX = Math.Min(minExtentX, point.X);
                minExtent.Y = Math.Min(minExtent.Y, point.Y);
            }
            
            foreach (var point in points.Skip(1))
            {
                minExtent.X = Math.Min(minExtent.X, point.X);
                minExtent.Y = Math.Min(minExtent.Y, point.Y);
            }
            
            var xMin = (int)Math.Max(point.X - radius, 0);
            var yMin = (int)Math.Max(point.Y - radius, 0);
            var xMax = (int)Math.Min(point.X + radius, map.Width - 1);
            var yMax = (int)Math.Min(point.Y + radius, map.Height - 1);

            var rectangle = new Rectangle(
                new Point(xMin, yMin),
                new Point(xMax, yMax)
            );

            return rectangle;
        }

        public static Func<Point, Point, uint, bool> MinSeparationFrom =
            (p1, p2, minDistance) => Distance.Chebyshev.Calculate(p1, p2) >= minDistance;
    }
}
