using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Print
{
    public class TableBuilder : ITableBuilder
    {
        private const int MARGIN_LEFT_AND_RIGHT = 1;

        private readonly List<Dictionary<ITableColumn, List<string>>> mCellContentByRowAndColumn =
            new List<Dictionary<ITableColumn, List<string>>>();

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
                    allColumnAndWidthMax[cellContentByCol.Key] = Math.Max(currentMax, cellContentByCol.Value.Max(txt => txt.Length));
                }
            }

            // and max the result with header width
            foreach (ITableColumn columnKey in allColumnAndWidthMax.Keys.ToArray())
            {
                int headerWidth = columnKey.ToString().Length;
                allColumnAndWidthMax[columnKey] = Math.Max(allColumnAndWidthMax[columnKey], headerWidth);
            }

            //--------------------
            // Fill table line by line, column by column
            //--------------------

            // HEADER
            foreach (KeyValuePair<ITableColumn, int> columnAndWidth in allColumnAndWidthMax)
            {
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                sb.Append(columnAndWidth.Key.ToString().PadRight(columnAndWidth.Value));
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
            }
            sb.AppendLine();

            foreach (Dictionary<ITableColumn, List<string>> row in mCellContentByRowAndColumn)
            {
                foreach (KeyValuePair<ITableColumn, int> col in allColumnAndWidthMax)
                {
                    int colWidth = allColumnAndWidthMax[col.Key];

                    List<string> cellContent;
                    {
                        row.TryGetValue(col.Key, out cellContent);
                        cellContent = cellContent ?? new List<string>();
                    }

                    string aggregatedCellContent = string.Join("/", cellContent);
                    if (cellContent.Count > 1)
                        aggregatedCellContent = "!! " + aggregatedCellContent;

                    sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                    sb.Append(aggregatedCellContent.PadRight(colWidth));
                    sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                }
                sb.AppendLine();
            }
        }
    }
}