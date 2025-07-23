namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns true if the enum value matches any of the provided values.
        /// </summary>
        public static bool In<T>(this T item, params T[] values) where T : struct, Enum
        {
            foreach (var value in values)
            {
                if (item.Equals(value))
                    return true;
            }
            return false;
        }

        public static bool NotIn<T>(this T item, params T[] values) where T : struct, Enum
        {
            foreach (var value in values)
            {
                if (item.Equals(value))
                    return false;
            }
            return true;
        }
    }
}
