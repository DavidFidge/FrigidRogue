using Microsoft.Xna.Framework;

using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;
using Point = SadRogue.Primitives.Point;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class PointExtensions
    {
        public static List<Point> Neighbours<T>(this Point point, IGridView<T> settableGridView, AdjacencyRule.Types adjacencyRule = AdjacencyRule.Types.EightWay)
        {
            return point.Neighbours(0, settableGridView.Width, 0, settableGridView.Height, adjacencyRule);
        }

        public static List<Point> Neighbours(this Point point, int xMax, int yMax, AdjacencyRule.Types adjacencyRule = AdjacencyRule.Types.EightWay)
        {
            return point.Neighbours(0, xMax, 0, yMax, adjacencyRule);
        }

        public static List<Point> Neighbours(this Point centrePoint, int? xMin = null, int? xMax = null,
            int? yMin = null, int? yMax = null, AdjacencyRule.Types adjacencyRule = AdjacencyRule.Types.EightWay)
        {
            var pointList = new List<Point>();

            for (var x = Math.Max(xMin ?? centrePoint.X - 1, centrePoint.X - 1);
                x <= Math.Min(xMax ?? centrePoint.X + 1, centrePoint.X + 1);
                x++)
            {
                for (var y = Math.Max(yMin ?? centrePoint.Y - 1, centrePoint.Y - 1);
                    y <= Math.Min(yMax ?? centrePoint.Y + 1, centrePoint.Y + 1);
                    y++)
                {
                    var point = new Point(x, y);

                    if (point != centrePoint
                        && (adjacencyRule == AdjacencyRule.Types.EightWay ||
                            point.IsNextTo(centrePoint, adjacencyRule)))
                    {
                        pointList.Add(point);
                    }
                }
            }

            return pointList;
        }

        public static List<Point> NeighboursOutwardsFrom<T>(this Point point, int distance, IGridView<T> settableGridView)
        {
            return point.NeighboursOutwardsFrom(0, settableGridView.Width, 0, settableGridView.Height);
        }

        public static List<Point> NeighboursOutwardsFrom(this Point point, int distance, int xMax, int yMax)
        {
            return point.NeighboursOutwardsFrom(0, xMax, 0, yMax);
        }

        public static List<Point> NeighboursOutwardsFrom(this Point centrePoint, int distance, int? xMin = null, int? xMax = null,
            int? yMin = null, int? yMax = null)
        {
            var pointList = new List<Point>();

            for (var x = Math.Max(xMin ?? centrePoint.X - distance, centrePoint.X - distance);
                 x <= Math.Min(xMax ?? centrePoint.X + distance, centrePoint.X + distance);
                 x++)
            {
                for (var y = Math.Max(yMin ?? centrePoint.Y - distance, centrePoint.Y - distance);
                     y <= Math.Min(yMax ?? centrePoint.Y + distance, centrePoint.Y + distance);
                     y++)
                {
                    var point = new Point(x, y);

                    if (point != centrePoint)
                    {
                        pointList.Add(point);
                    }
                }
            }


            return pointList;
        }

        public static List<Point> PointsOutwardsFrom(this Point centrePoint, int distance, int? xMin = null, int? xMax = null, int? yMin = null, int? yMax = null)
        {
            var pointList = new List<Point>();

            if ((xMax == null || centrePoint.X - distance <= xMax) && (xMin == null || centrePoint.X - distance >= xMin))
                pointList.Add(new Point(centrePoint.X - distance, centrePoint.Y));

            if ((xMax == null || centrePoint.X + distance <= xMax) && (xMin == null || centrePoint.X + distance >= xMin))
                pointList.Add(new Point(centrePoint.X + distance, centrePoint.Y));

            if ((yMax == null || centrePoint.Y - distance <= yMax) && (yMin == null || centrePoint.Y - distance >= yMin))
                pointList.Add(new Point(centrePoint.X, centrePoint.Y - distance));

            if ((yMax == null || centrePoint.Y + distance <= yMax) && (yMin == null || centrePoint.Y + distance >= yMin))
                pointList.Add(new Point(centrePoint.X, centrePoint.Y + distance));

            return pointList;
        }

        public static Tuple<IList<Point>, IList<Point>> SplitIntoPointsBySumMagnitudeAgainstTargetPoints(
            this IList<Point> pointsToSplit,
            IList<Point> targetPoints,
            float splittingPoint = 0.5f
        )
        {
            var pointsToSplitWithMagnitude = pointsToSplit
                .Select(p => (Point: p, Magnitude: targetPoints.Select(t => Point.EuclideanDistanceMagnitude(p, t)).Sum()))
                .OrderBy(p => p.Magnitude)
                .ToList();

            var splitIndex = pointsToSplitWithMagnitude.Count;

            if (splittingPoint > 1)
                splittingPoint = 1;

            if (splittingPoint < 0)
                splittingPoint = 0;

            if (splitIndex > 1)
                splitIndex = (int)(splitIndex * splittingPoint);

            return new Tuple<IList<Point>, IList<Point>>(
                pointsToSplitWithMagnitude.Take(splitIndex).Select(w => w.Point).ToList(),
                pointsToSplitWithMagnitude.Skip(splitIndex).Select(w => w.Point).ToList()
            );
        }

        public static Vector2 ToVector(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point FromVectorRounded(this Vector2 vector2)
        {
            return new Point((int)Math.Round(vector2.X, 0), (int)Math.Round(vector2.Y, 0));
        }

        public static Point GetMidpoint(this IEnumerable<Point> points)
        {
            return new Point((int)points.Average(p => p.X), (int)points.Average(p => p.Y));
        }

        public static bool IsNextTo(this Point point1, Point point2, AdjacencyRule adjacencyRule)
        {
            if (point1.Matches(point2))
                return false;

            if (adjacencyRule == AdjacencyRule.EightWay)
            {
                if (Math.Abs(point1.X - point2.X) <= 1 && Math.Abs(point1.Y - point2.Y) <= 1)
                    return true;
            }

            else if (adjacencyRule == AdjacencyRule.Cardinals)
            {
                var x = Math.Abs(point1.X - point2.X);
                var y = Math.Abs(point1.Y - point2.Y);

                if ((x == 1 && y == 0) || (x == 0 && y == 1))
                    return true;
            }

            else if (adjacencyRule == AdjacencyRule.Diagonals)
            {
                var x = Math.Abs(point1.X - point2.X);
                var y = Math.Abs(point1.Y - point2.Y);

                if (x == 1 && y == 1)
                    return true;
            }

            return false;
        }
    }
}
