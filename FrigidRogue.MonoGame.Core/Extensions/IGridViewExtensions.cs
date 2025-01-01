using System.Text;
using GoRogue.Pathing;
using SadRogue.Primitives;
using SadRogue.Primitives.GridViews;

namespace FrigidRogue.MonoGame.Core.Extensions
{
    public static class IGridViewExtensions
    {
        public static void AddToStringBuilder<T>(this IGridView<T> gridView, StringBuilder stringBuilder, string elementSeparator = " ", int? fieldSize = 0)
        {
            string str;
            if (fieldSize != null)
                str = gridView.ExtendToString(elementSeparator: elementSeparator, rowSeparator: Environment.NewLine, elementStringifier: (obj, x, y) => obj?.ToString() ?? "-", fieldSize: fieldSize.Value);
            else
                str = gridView.ExtendToString(elementSeparator: elementSeparator, rowSeparator: Environment.NewLine, elementStringifier: (obj, x, y) => obj?.ToString() ?? "-");

            var lines = str.Split(Environment.NewLine);

            foreach (var line in lines)
                stringBuilder.AppendLine(line);
        }
        
        public static void AddToStringBuilderWithPath<T>(this IGridView<T> gridView, StringBuilder stringBuilder, Path path)
        {
            var str = gridView.ExtendToString(rowSeparator: Environment.NewLine, elementStringifier: (obj, x, y) =>
            {
                var point = new Point(x, y);
                if (path.Steps.Contains(point))
                    return "P";
                if (path.Start == point)
                    return "S";
                
                return obj?.ToString() ?? "-";
            });

            var lines = str.Split(Environment.NewLine);

            foreach (var line in lines)
                stringBuilder.AppendLine(line);
        }
        
        public static void AddToStringBuilderGrid<T>(this IGridView<T> gridView, StringBuilder stringBuilder, int gridCellWidth, Func<T, string> elementStringifierValues = null)
        {
            elementStringifierValues ??= obj => obj?.ToString() ?? "null";

            var str = gridView.ExtendToString(fieldSize: gridCellWidth, rowSeparator: Environment.NewLine, elementStringifier: elementStringifierValues);

            var lines = str.Split(Environment.NewLine);

            foreach (var line in lines)
                stringBuilder.AppendLine(line);
        }
        
        public static void AddToStringBuilderWithPathGrid<T>(this IGridView<T> gridView, StringBuilder stringBuilder, Path path, int gridCellWidth, Func<T, string> elementStringifierValues = null)
        {
            elementStringifierValues ??= obj => obj?.ToString() ?? "null";
            
            var str = gridView.ExtendToString(fieldSize: gridCellWidth, rowSeparator: Environment.NewLine, elementStringifier: (obj, x, y) =>
            {
                var point = new Point(x, y);
                if (path.Steps.Contains(point))
                    return "P";
                if (path.Start == point)
                    return "S";

                return elementStringifierValues(obj);
            });

            var lines = str.Split(Environment.NewLine);

            foreach (var line in lines)
                stringBuilder.AppendLine(line);
        }
        
        /// <summary>
        /// Allows stringifying the contents of a grid view. Takes characters to surround the grid view printout, and
        /// each row, the method used to get the string representation of each element (defaulting to the ToString
        /// function of type T), and separation characters for each element and row.
        /// </summary>
        /// <typeparam name="T" />
        /// <param name="gridView" />
        /// <param name="begin">Character(s) that should precede the IGridView printout.</param>
        /// <param name="beginRow">Character(s) that should precede each row.</param>
        /// <param name="elementStringifier">
        /// Function to use to get the string representation of each value. null uses the ToString
        /// function of type T.
        /// </param>
        /// <param name="rowSeparator">Character(s) to separate each row from the next.</param>
        /// <param name="elementSeparator">Character(s) to separate each element from the next.</param>
        /// <param name="endRow">Character(s) that should follow each row.</param>
        /// <param name="end">Character(s) that should follow the IGridView printout.</param>
        /// <returns>A string representation of the values in the grid view.</returns>
        public static string ExtendToString<T>(this IGridView<T> gridView, string begin = "", string beginRow = "",
                                               Func<T, int, int, string> elementStringifier = null,
                                               string rowSeparator = "\n", string elementSeparator = " ",
                                               string endRow = "", string end = "")
        {
            elementStringifier ??= (obj, x, y) => obj?.ToString() ?? "null";

            var result = new StringBuilder(begin);
            for (var y = 0; y < gridView.Height; y++)
            {
                result.Append(beginRow);
                for (var x = 0; x < gridView.Width; x++)
                {
                    result.Append(elementStringifier(gridView[x, y], x, y));
                    if (x != gridView.Width - 1) result.Append(elementSeparator);
                }

                result.Append(endRow);
                if (y != gridView.Height - 1) result.Append(rowSeparator);
            }

            result.Append(end);

            return result.ToString();
        }

        /// <summary>
        /// Allows stringifying the contents of a grid view. Takes characters to surround the grid view representation,
        /// and each row, the method used to get the string representation of each element (defaulting to the ToString
        /// function of type T), and separation characters for each element and row. Takes the size of the field to
        /// give each element, characters to surround the GridView printout, and each row, the method used to get the
        /// string representation of each element (defaulting to the ToString function of type T), and separation
        /// characters for each element and row.
        /// </summary>
        /// <typeparam name="T" />
        /// <param name="gridView" />
        /// <param name="fieldSize">
        /// The amount of space each element should take up in characters. A positive number aligns
        /// the text to the right of the space, while a negative number aligns the text to the left.
        /// </param>
        /// <param name="begin">Character(s) that should precede the IGridView printout.</param>
        /// <param name="beginRow">Character(s) that should precede each row.</param>
        /// <param name="elementStringifier">
        /// Function to use to get the string representation of each value. Null uses the ToString
        /// function of type T.
        /// </param>
        /// <param name="rowSeparator">Character(s) to separate each row from the next.</param>
        /// <param name="elementSeparator">Character(s) to separate each element from the next.</param>
        /// <param name="endRow">Character(s) that should follow each row.</param>
        /// <param name="end">Character(s) that should follow the IGridView printout.</param>
        /// <returns>A string representation of the grid view.</returns>
        public static string ExtendToString<T>(this IGridView<T> gridView, int fieldSize, string begin = "",
                                               string beginRow = "", Func<T, int, int, string> elementStringifier = null,
                                               string rowSeparator = "\n", string elementSeparator = " ",
                                               string endRow = "", string end = "")
        {
            elementStringifier ??= (obj, x, y) => obj?.ToString() ?? "null";

            var result = new StringBuilder(begin);
            for (var y = 0; y < gridView.Height; y++)
            {
                result.Append(beginRow);
                for (var x = 0; x < gridView.Width; x++)
                {
                    result.Append(string.Format($"{{0, {fieldSize}}}", elementStringifier(gridView[x, y], x, y)));
                    if (x != gridView.Width - 1) result.Append(elementSeparator);
                }

                result.Append(endRow);
                if (y != gridView.Height - 1) result.Append(rowSeparator);
            }

            result.Append(end);

            return result.ToString();
        }
        
        public static ArrayView<T> Subset<T>(this IGridView<T> gridView, Rectangle rectangle)
        {
            var subset = new ArrayView<T>(rectangle.Width, rectangle.Height);

            for (var y = 0; y < rectangle.Height; y++)
            {
                for (var x = 0; x < rectangle.Width; x++)
                {
                    subset[x, y] = gridView[x + rectangle.MinExtentX, y + rectangle.MinExtentY];
                }
            }

            return subset;
        }
    }
}