using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class TableBuilder : ITableBuilder
    {
        private readonly List<Dictionary<ITableColumn, List<string>>> mCellContentByRowAndColumn =
            new List<Dictionary<ITableColumn, List<string>>>();

        private readonly int MARGIN_LEFT_AND_RIGHT = 1;
        private readonly string COLUMN_SEPARATOR = "|";
        private readonly char HEADER_CONTENT_SEPARATOR = '-';

        public void NewRow()
        {
            mCellContentByRowAndColumn.Add(new Dictionary<ITableColumn, List<string>>());
        }

        public void Print(ITableColumn tableColumn, string format, params object[] args)
        {
            string cellContent = string.Format(format, args);

            Dictionary<ITableColumn, List<string>> rowContent = mCellContentByRowAndColumn.Last();

            List<string> strings;
            if (!rowContent.TryGetValue(tableColumn, out strings))
            {
                strings = new List<string>();
                rowContent.Add(tableColumn, strings);
            }

            strings.Add(cellContent);
        }

        public void GenerateTable(StringBuilder sb)
        {
            //--------------------
            // Calculate Column Width 
            //--------------------

            // Get max over all column cells
            var allColumnAndWidthMax = new Dictionary<ITableColumn, int>();
            foreach (Dictionary<ITableColumn, List<string>> row in mCellContentByRowAndColumn)
            {
                foreach (KeyValuePair<ITableColumn, List<string>> cellContentByCol in row)
                {
                    int currentMax;
                    allColumnAndWidthMax.TryGetValue(cellContentByCol.Key, out currentMax);
                    allColumnAndWidthMax[cellContentByCol.Key] = Math.Max(currentMax,
                                                                          cellContentByCol.Value.Max(txt => txt.Length));
                }
            }

            // and max the result with header width
            foreach (ITableColumn columnKey in allColumnAndWidthMax.Keys.ToArray())
            {
                int headerWidth = columnKey.Title.Length;
                allColumnAndWidthMax[columnKey] = Math.Max(allColumnAndWidthMax[columnKey], headerWidth);
            }

            //--------------------
            // Fill table line by line, column by column
            //--------------------

            PrintRow(allColumnAndWidthMax, sb, (column, width) => column.Title);
            PrintRow(allColumnAndWidthMax, sb, (column, width) => new string(HEADER_CONTENT_SEPARATOR, width));

            foreach (Dictionary<ITableColumn, List<string>> row in mCellContentByRowAndColumn)
            {
                Dictionary<ITableColumn, List<string>> row1 = row;
                PrintRow(allColumnAndWidthMax, sb, (col, width) => AggregatedCellContent(row1, col));
            }
        }

        private void PrintRow(Dictionary<ITableColumn, int> allColumnAndWidthMax,
                              StringBuilder sb,
                              Func<ITableColumn, int, string> printCellContent
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
                sb.Append(Align(printCellContent(column, width), width, column.HorizontalAlignment));
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));

                isFirstColumn = false;
            }
            sb.AppendLine();
        }

        private static string AggregatedCellContent(Dictionary<ITableColumn, List<string>> row, ITableColumn col)
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

        private string Align(string cellContent, int width, HorizontalAlignment horizontalAlignment)
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