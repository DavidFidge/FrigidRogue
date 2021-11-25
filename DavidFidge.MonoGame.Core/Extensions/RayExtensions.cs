using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Extensions
{
    public static class RayExtensions
    {
        public static Ray ClipToZ(this Ray ray, float from, float to)
        {
            if (ray.Direction.Z < 0.0001f && ray.Direction.Z > -0.0001f)
                return ray;

            var min = from < to ? from : to;
            var max = from > to ? from : to;

            var oldStartPoint = ray.Position;

            var factorH = (max - oldStartPoint.Z) / ray.Direction.Z;
            var pointA = oldStartPoint + (factorH * ray.Direction);
            var factorL = (min - oldStartPoint.Z) / ray.Direction.Z;
            var pointB = oldStartPoint + (factorL * ray.Direction);
            var newDirection = pointB - pointA;

            return new Ray(pointA, newDirection);
        }
    }
}
