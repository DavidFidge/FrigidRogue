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

        public static Func<Point, Point, uint, bool> MinSeparationFrom =
            (p1, p2, minDistance) => Distance.Chebyshev.Calculate(p1, p2) >= minDistance;
    }
}
