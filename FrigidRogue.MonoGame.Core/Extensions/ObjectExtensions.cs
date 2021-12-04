using System;
using System.Linq;
using System.Reflection;
using System.Text;

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
    }
}
