using GoRogue.GameFramework;
using GoRogue.Random;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using ShaiRandom.Generators;
using SharpDX;
using Point = SadRogue.Primitives.Point;
using Rectangle = SadRogue.Primitives.Rectangle;

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
            var rectangle = Rectangle.WithRadius(point, (int)radius, (int)radius);

            rectangle = Rectangle.GetIntersection(rectangle, map.Bounds());

            return rectangle;
        }

        public static Rectangle RectangleCoveringPoints(this Map map, params Point[] points)
        {
            if (points == null || points.Length == 0)
            {
                return new Rectangle(
                    new Point(0, 0),
                    new Point(map.Width - 1, map.Height - 1)
                );
            }

            var rectangle = RectangleExtensions.CoveringPoints(points);

            rectangle = Rectangle.GetIntersection(rectangle, map.Bounds());
            
            return rectangle;
        }

        public static Func<Point, Point, uint, bool> MinSeparationFrom =
            (p1, p2, minDistance) => Distance.Chebyshev.Calculate(p1, p2) >= minDistance;
    }
}
