using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class RectangleExtensions
    {
        public static IEnumerable<Point> PerimeterBorder(this Rectangle rectangle, int borderSize = 1)
        {
            var points = new HashSet<Point>((rectangle.Width * 2 + rectangle.Height * 2) * borderSize);

            for (var i = 0; i < borderSize; i++)
            {
                points.UnionWith(rectangle.PerimeterPositions());

                if (i + 1 < borderSize)
                    rectangle = rectangle.Expand(-1, -1);
            }

            return points;
        }

        public static Rectangle WithExtentsUnordered(Point point1, Point point2)
        {
            return new Rectangle(
                new Point(Math.Min(point1.X, point2.X), Math.Min(point1.Y, point2.Y)),
                new Point(Math.Max(point1.X, point2.X), Math.Max(point1.Y, point2.Y)));
        }

        public static Rectangle CoveringPoints(params Point[] points)
        {
            if (points == null || points.Length == 0)
                return Rectangle.Empty;

            if (points.Length == 1)
                return new Rectangle(points[0], points[0]);

            var minExtentX = points[0].X;
            var maxExtentX = points[0].X;
            var minExtentY = points[0].Y;
            var maxExtentY = points[0].Y;

            foreach (var point in points.Skip(1))
            {
                minExtentX = Math.Min(point.X, minExtentX);
                maxExtentX = Math.Max(point.X, maxExtentX);
                minExtentY = Math.Min(point.Y, minExtentY);
                maxExtentY = Math.Max(point.Y, maxExtentY);
            }

            return new Rectangle(
                new Point(minExtentX, minExtentY),
                new Point(maxExtentX, maxExtentY));
        }
    }
}
