using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Print
{
    public class PrintCaseTableDirector : ActionDirector<INode, IPrintCaseTableDirector>, IPrintCaseTableDirector
    {
        #region inner types

        private class CellData
        {
            private readonly CodeLocation mCodeLocation;
            private readonly string mContent;

            public CellData(CodeLocation codeLocation, string content)
            {
                mCodeLocation = codeLocation;
                mContent = content;
            }

            public CodeLocation CodeLocation
            {
                get { return mCodeLocation; }
            }

            public string Content
            {
                get { return mContent; }
            }
        }

        #endregion

        private const int MARGIN_LEFT_AND_RIGHT = 1;

        private readonly List<Dictionary<object, List<CellData>>> mCellsByRowAndColumn =
            new List<Dictionary<object, List<CellData>>>();

        public PrintCaseTableDirector(IActionVisitMapper<INode, IPrintCaseTableDirector> visitMapper)
            : base(visitMapper)
        {
        }

        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFileInfo { get; set; }

        public void NewRow()
        {
            mCellsByRowAndColumn.Add(new Dictionary<object, List<CellData>>());
        }

        public void Print(CodeLocation codeLocation, object column, string format, params object[] args)
        {
            string cellContent = string.Format(format, args);

            Dictionary<object, List<CellData>> rowContent = mCellsByRowAndColumn.Last();

            List<CellData> cellDatas;
            if (!rowContent.TryGetValue(column, out cellDatas))
            {
                cellDatas = new List<CellData>();
                rowContent.Add(column, cellDatas);
            }

            cellDatas.Add(new CellData(codeLocation, cellContent));
        }

        public string GetString()
        {
            //--------------------
            // Calculate Column Width 
            //--------------------
            var allColumnAndWidthMax = new Dictionary<object, int>();
            foreach (Dictionary<object, List<CellData>> row in mCellsByRowAndColumn)
            {
                foreach (KeyValuePair<object, List<CellData>> colAndCell in row)
                {
                    int currentMax;
                    allColumnAndWidthMax.TryGetValue(colAndCell.Key, out currentMax);
                    allColumnAndWidthMax[colAndCell.Key] = Math.Max(currentMax,
                                                                    colAndCell.Value.Max(cellData => cellData.Content.Length));
                }
            }

            foreach (object columnKey in allColumnAndWidthMax.Keys.ToArray())
            {
                int headerWidth = columnKey.ToString().Length;
                allColumnAndWidthMax[columnKey] = Math.Max(allColumnAndWidthMax[columnKey], headerWidth);
            }

            //--------------------
            // Fill table line by line, column by column
            //--------------------

            var sb = new StringBuilder();
            
            // HEADER
            foreach (KeyValuePair<object, int> columnAndWidth in allColumnAndWidthMax)
            {
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                sb.Append(columnAndWidth.Key.ToString().PadRight(columnAndWidth.Value));
                sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
            }
            sb.AppendLine();

            int amountCases = 0;
            foreach (Dictionary<object, List<CellData>> row in mCellsByRowAndColumn)
            {
                amountCases++;
                foreach (KeyValuePair<object, int> col in allColumnAndWidthMax)
                {
                    int colWidth = allColumnAndWidthMax[col.Key];

                    List<CellData> cellDatas;
                    {
                        row.TryGetValue(col.Key, out cellDatas);
                        cellDatas = cellDatas ?? new EditableList<CellData>();
                    }

                    string cellContent = string.Join("/", cellDatas.Select(cd => cd.Content));
                    if (cellDatas.Count > 1)
                        cellContent = "!! " + cellContent;

                    sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                    sb.Append(cellContent.PadRight(colWidth));
                    sb.Append(new string(' ', MARGIN_LEFT_AND_RIGHT));
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendFormat("TOTAL: {0} TEST CASES", amountCases);

            return sb.ToString();
        }
    }
}