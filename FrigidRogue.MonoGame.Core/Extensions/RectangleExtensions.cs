using System.Collections.Generic;

using SadRogue.Primitives;

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
    }
}
