using GoRogue.Pathing;
using Point = SadRogue.Primitives.Point;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class PathExtensions
    {
        public static Point GetStepAfterPoint(this Path path, Point point)
        {
            var enumerator = path.Steps.GetEnumerator();

            if (!enumerator.MoveNext())
                return Point.None;

            while (true)
            {
                if (enumerator.Current == point)
                {
                    var hasNextValue = enumerator.MoveNext();

                    return hasNextValue ? enumerator.Current : Point.None;
                }

                if (!enumerator.MoveNext())
                    return Point.None;
            }
        }
        
        public static Point GetStepAfterPointWithStart(this Path path, Point point)
        {
            var enumerator = path.StepsWithStart.GetEnumerator();

            if (!enumerator.MoveNext())
                return Point.None;

            while (true)
            {
                if (enumerator.Current == point)
                {
                    var hasNextValue = enumerator.MoveNext();

                    return hasNextValue ? enumerator.Current : Point.None;
                }

                if (!enumerator.MoveNext())
                    return Point.None;
            }
        }
        
        
    }
}
