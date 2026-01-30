using System.DirectoryServices;

using SadRogue.Primitives;

using ShaiRandom.Collections;
using ShaiRandom.Generators;

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

        public static List<Point> WiggleDig(this Rectangle bounds, Point start, Point end, IEnhancedRandom enhancedRandom)
        {
            var currentPoint = start;

            var distanceToTarget = Distance.Chebyshev.Calculate(start, end);

            var multiplier = 1;
            var probabilityTable = GetWeights(multiplier, start, end, enhancedRandom);

            var loops = 0;

            var points = new HashSet<Point>(bounds.Width * bounds.Height)
            {
                start
            };

            var tries = 0;

            while (tries < (bounds.Width * bounds.Height * 10))
            {
                tries++;

                var nextDirection = probabilityTable.NextItem();

                if (!bounds.Contains(currentPoint + nextDirection))
                {
                    var queue = GetQueueWithPreferredDirection(nextDirection);

                    for (var i = 1; i < queue.Length; i++)
                    {
                        nextDirection = queue[i];

                        if (bounds.Contains(currentPoint + nextDirection))
                            break;
                    }
                }

                currentPoint = currentPoint + nextDirection;

                points.Add(currentPoint);

                if (currentPoint == end)
                    break;

                loops++;

                if (loops > distanceToTarget / 2)
                {
                    start = currentPoint;
                    probabilityTable = GetWeights(multiplier++, start, end, enhancedRandom);
                    loops = 0;
                }
            }

            return points.ToList();
        }

        private static ProbabilityTable<Direction> GetWeights(int weightToEndMultiplier, Point start, Point end, IEnhancedRandom enhancedRandom)
        {
            var generalDirection = Direction.GetDirection(start, end);

            var weights = new List<(Direction direction, double weight)>
            {
                (generalDirection, 4 * weightToEndMultiplier),
                (generalDirection + 1, 3 * weightToEndMultiplier),
                (generalDirection - 1, 3 * weightToEndMultiplier),
                (generalDirection + 2, 2 * weightToEndMultiplier),
                (generalDirection - 2, 2 * weightToEndMultiplier),
                (generalDirection + 3, 1),
                (generalDirection - 3, 1),
                (generalDirection.Opposite(), 1)
            };

            var probabilityTable = new ProbabilityTable<Direction>(weights);
            probabilityTable.Random = enhancedRandom;

            return probabilityTable;
        }

        public static Direction[] GetQueueWithPreferredDirection(this Direction direction)
        {
            return new Direction[]
            {
                direction,
                direction + 1,
                direction - 1,
                direction + 2,
                direction - 2,
                direction + 3,
                direction - 3,
                direction.Opposite()
            };
        }
    }
}
