﻿using System;
using System.Collections.Generic;

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
    }
}