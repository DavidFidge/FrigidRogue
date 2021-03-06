using System;
using System.Collections.Generic;
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
    }
}
