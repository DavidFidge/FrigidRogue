using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class PointExtensions
    {
        public static List<Point> SurroundingPoints(this Point centrePoint, int? xMin = null, int? xMax = null, int? yMin = null, int? yMax = null)
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

                    if (point != centrePoint)
                        pointList.Add(point);
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

        public static Vector2 ToVector(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point GetMidpoint(this IEnumerable<Point> points)
        {
            return new Point((int)points.Average(p => p.X), (int)points.Average(p => p.Y));
        }
    }
}
