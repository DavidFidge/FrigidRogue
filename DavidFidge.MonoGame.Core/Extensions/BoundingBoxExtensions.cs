using System;

using Microsoft.Xna.Framework;

namespace DavidFidge.MonoGame.Core.Extensions
{
    public static class BoundingBoxExtensions
    {
        public static float Width(this BoundingBox boundingBox)
        {
            return Math.Abs(boundingBox.Max.X - boundingBox.Min.X);
        }

        public static float Length(this BoundingBox boundingBox)
        {
            return Math.Abs(boundingBox.Max.Y - boundingBox.Min.Y);
        }
    }
}
