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
        
        public static T[] Initialise<T>(this T[] array, Func<T> func)
        {
            for (var index = 0; index < array.Length; index++)
            {
                array[index] = func();
            }

            return array;
        }
        
        public static IList<T> Initialise<T>(this IList<T> list, Func<T> func)
        {
            for (var index = 0; index < list.Count; index++)
            {
                list[index] = func();
            }

            return list;
        }
    }
}
