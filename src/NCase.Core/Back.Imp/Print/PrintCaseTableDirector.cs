using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private readonly Dictionary<object, Dictionary<object, List<CellData>>> mCellsByRowAndColumn =
            new Dictionary<object, Dictionary<object, List<CellData>>>();

        public PrintCaseTableDirector(IActionVisitMapper<INode, IPrintCaseTableDirector> visitMapper)
            : base(visitMapper)
        {
        }

        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFileInfo { get; set; }

        public void Print(CodeLocation codeLocation, object row, object column, string format, params object[] args)
        {
            string cellContent = string.Format(format, args);

            Dictionary<object, List<CellData>> rowContent;
            if (!mCellsByRowAndColumn.TryGetValue(row, out rowContent))
            {
                rowContent = new Dictionary<object, List<CellData>>();
                mCellsByRowAndColumn.Add(row, rowContent);
            }

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
            // Get Column Width and Row Width
            Dictionary<object, int> columnAndColumnWidths = new Dictionary<object, int>();
            foreach (Dictionary<object, List<CellData>> row in mCellsByRowAndColumn.Values)
            {
                foreach (KeyValuePair<object, List<CellData>> colAndCell in row)
                {
                    int currentMax;
                    columnAndColumnWidths.TryGetValue(colAndCell.Key, out currentMax);
                    columnAndColumnWidths[colAndCell.Key] = Math.Max(currentMax, colAndCell.Value.Max(cellData => cellData.Content.Length));
                }
            }

            var sb = new StringBuilder();
            foreach (Dictionary<object, List<CellData>> row in mCellsByRowAndColumn.Values)
            {
                foreach (KeyValuePair<object, int> col in columnAndColumnWidths)
                {
                    List<CellData> cellDatas = row[col.Key];

                    sb.AppendFormat()
                    columnAndColumnWidths[colAndCell.Key] = colAndCell.Value.Max(cellData => cellData.Content.Length);
                }
            }

        }
    }
}