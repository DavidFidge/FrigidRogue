using System.Collections.Generic;
using System.Linq;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string PropertiesToString(this object data)
        {
            if (data == null)
                return "null";

            return data
                .GetType()
                .GetProperties()
                .Select(p => $"{p.Name}: {p.GetValue(data)}")
                .ToCsv();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.IsEmpty();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }
    }
}
