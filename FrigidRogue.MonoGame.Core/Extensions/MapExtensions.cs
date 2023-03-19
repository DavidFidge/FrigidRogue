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

        public static Func<Point, Point, uint, bool> MinSeparationFrom =
            (p1, p2, minDistance) => Distance.Chebyshev.Calculate(p1, p2) >= minDistance;
    }
}
