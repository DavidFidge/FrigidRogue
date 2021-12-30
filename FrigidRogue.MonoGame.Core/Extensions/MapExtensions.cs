using System;
using System.Collections.Generic;
using GoRogue.GameFramework;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class MapExtensions
    {
        public static Point RandomPositionAwayFrom(this Map map, Point pointAwayFrom, uint minDistance, Func<Point, IEnumerable<IGameObject>, bool> selector)
        {
            return map.RandomPosition(
                (p, gameObjects) =>
                    MinSeparationFrom(pointAwayFrom, p, minDistance) &&
                    selector(p, gameObjects)
            );
        }

        public static Func<Point, Point, uint, bool> MinSeparationFrom =
            (p1, p2, minDistance) => Distance.Chebyshev.Calculate(p1, p2) >= minDistance;
    }
}
