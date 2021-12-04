using System.Reflection;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetPropertyInfo(this object data, string property)
        {
            return data.GetType().GetProperty(property);
        }
    }
}
