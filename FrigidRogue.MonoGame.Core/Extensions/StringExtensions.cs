using System.Text.RegularExpressions;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCsv(this IEnumerable<string> strings)
        {
            return strings.Join(", ");
        }

        public static string Join(this IEnumerable<string> strings, string separator)
        {
            return String.Join(separator, strings);
        }

        public static string AddSpaces(this string s)
        {
            return Regex.Replace(s, @"\B[A-Z]", " $0");
        }
        
        public static string RemoveSpaces(this string s)
        {
            return Regex.Replace(s, @"\s", string.Empty);
        }

        public static string ToSeparateWords(this string s)
        {
            return Regex.Replace(s, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
