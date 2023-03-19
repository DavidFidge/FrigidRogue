using System.Text;
using System.Text.RegularExpressions;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class IGridViewExtensions
    {
        public static void AddToStringBuilder<T>(this IGridView<T> gridView, StringBuilder stringBuilder)
        {
            var str = gridView.ExtendToString(rowSeparator: Environment.NewLine);

            str = Regex.Replace(str, "null", "-");

            var lines = str.Split(Environment.NewLine);

            foreach (var line in lines)
                stringBuilder.AppendLine(line);
        }
    }
}
