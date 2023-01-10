using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class Vector4Extensions
    {

        public static Vector3 XYZ(this Vector4 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static float Index(this Vector4 vector, int index)
        {
            switch (index)
            {
                case 0: return vector.X;
                case 1: return vector.Y;
                case 2: return vector.Z;
                case 3: return vector.W;
                default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, 2 or 3.");
            }
        }

        public static void SetIndex(this Vector4 vector, int index, float value)
        {
            switch (index)
            {
                case 0:
                    vector.X = value;
                    break;
                case 1:
                    vector.Y = value;
                    break;
                case 2:
                    vector.Z = value;
                    break;
                case 3:
                    vector.W = value;
                    break;
                default: throw new ArgumentOutOfRangeException("index", "The index is out of range. Allowed values are 0, 1, 2 or 3.");
            }
        }
    }
}
