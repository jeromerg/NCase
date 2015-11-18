using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NDsl.Back.Api.Util.Table;

namespace NDsl.Back.Imp.Util.Table
{
    public class TableBuilder : ITableBuilder
    {
        private const int MARGIN_LEFT_AND_RIGHT = 1;
        private const string COLUMN_SEPARATOR = "|";
        private const char HEADER_CONTENT_SEPARATOR = '-';

        [NotNull, ItemNotNull] private readonly List<Dictionary<ITableColumn, List<string>>> mCellContentByRowAndColumn =
            new List<Dictionary<ITableColumn, List<string>>>();

        public void NewRow()
        {
            mCellContentByRowAndColumn.Add(new Dictionary<ITableColumn, List<string>>());
        }

        public void Print([NotNull] ITableColumn tableColumn, [NotNull] string format, [NotNull] params object[] args)
        {
            if (tableColumn == null) throw new ArgumentNullException("tableColumn");
            if (format == null) throw new ArgumentNullException("format");
            if (args == null) throw new ArgumentNullException("args");

            string cellContent = string.Format(format, args);

            Dictionary<ITableColumn, List<string>> rowContent = mCellContentByRowAndColumn.Last();

            List<string> strings;
            // ReSharper disable once PossibleNullReferenceException
            if (!rowContent.TryGetValue(tableColumn, out strings))
            {
                strings = new List<string>();
                rowContent.Add(tableColumn, strings);
            }

            // ReSharper disable once PossibleNullReferenceException
            strings.Add(cellContent);
        }

        public void GenerateTable([NotNull] StringBuilder sb)
        {
            if (sb == null) throw new ArgumentNullException("sb");

            //--------------------
            // Calculate Column Width 
            //--------------------

            // Get max over all column cells
            var allColumnAndWidthMax = new Dictionary<ITableColumn, int>();
            foreach (Dictionary<ITableColumn, List<string>> row in mCellContentByRowAndColumn)
            {
                foreach (KeyValuePair<ITableColumn, List<string>> cellContentByCol in row)
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    // ReSharper disable PossibleNullReferenceException

                    int currentMax;
                    allColumnAndWidthMax.TryGetValue(cellContentByCol.Key, out currentMax);
                    allColumnAndWidthMax[cellContentByCol.Key] = Math.Max(currentMax,
                                                                          cellContentByCol.Value.Max(txt => txt.Length));

                    // ReSharper restore PossibleNullReferenceException
                    // ReSharper restore AssignNullToNotNullAttribute
                }
            }

            // and max the result with header width
            foreach (ITableColumn columnKey in allColumnAndWidthMax.Keys.ToArray())
            {
                // ReSharper disable once PossibleNullReferenceException
                int headerWidth = columnKey.Title.Length;
                allColumnAndWidthMax[columnKey] = Math.Max(allColumnAndWidthMax[columnKey], headerWidth);
            }

            //--------------------
            // Fill table line by line, column by column
            //--------------------

            // ReSharper disable once PossibleNullReferenceException
            PrintRow(allColumnAndWidthMax, sb, (column, width) => column.Title);
            PrintRow(allColumnAndWidthMax, sb, (column, width) => new string(HEADER_CONTENT_SEPARATOR, width));

            foreach (Dictionary<ITableColumn, List<string>> row in mCellContentByRowAndColumn)
            {
                Dictionary<ITableColumn, List<string>> row1 = row;
                // ReSharper disable once AssignNullToNotNullAttribute
                PrintRow(allColumnAndWidthMax, sb, (col, width) => AggregatedCellContent(row1, col));
            }
        }

        private void PrintRow([NotNull] Dictionary<ITableColumn, int> allColumnAndWidthMax,
                              [NotNull] StringBuilder sb,
                              [NotNull] Func<ITableColumn, int, string> printCellContent
            )
        {
            bool isFirstColumn = true;
            foreach (KeyValuePair<ITableColumn, int> columnAndWidth in allColumnAndWidthMax)
            {
                ITableColumn column = columnAndWidth.Key;
                int width = columnAndWidth.Value;

                if (!isFirstColumn)
                    sb.Append(COLUMN_SEPARATOR);
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                sb.Append(Align(printCellContent(column, width), width, column.HorizontalAlignment));
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));

                isFirstColumn = false;
            }
            sb.AppendLine();
        }

        private static string AggregatedCellContent([NotNull] Dictionary<ITableColumn, List<string>> row, [NotNull] ITableColumn col)
        {
            List<string> cellContent;
            {
                row.TryGetValue(col, out cellContent);
                cellContent = cellContent ?? new List<string>();
            }

            string aggregatedCellContent = string.Join("/", cellContent);
            if (cellContent.Count > 1)
                aggregatedCellContent = "!! " + aggregatedCellContent;

            return aggregatedCellContent;
        }

        private string Align([NotNull] string cellContent, int width, HorizontalAlignment horizontalAlignment)
        {
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    return cellContent.PadRight(width);
                case HorizontalAlignment.Center:
                    return cellContent.PadLeft(((width - cellContent.Length)/2) + cellContent.Length).PadRight(width);
                case HorizontalAlignment.Right:
                    return cellContent.PadLeft(width);
                default:
                    throw new ArgumentOutOfRangeException("horizontalAlignment", horizontalAlignment, null);
            }
        }
    }
}