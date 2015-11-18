using System;
using System.Text;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintCaseTableDirector : ActionDirector<INode, IPrintCaseTableDirector>, IPrintCaseTableDirector
    {
        [NotNull] private static readonly SimpleTableColumn sIndexClumn = new SimpleTableColumn("#", HorizontalAlignment.Right);

        [NotNull] private readonly ITableBuilder mTableBuilder;
        private int mAmountOfCases;

        public PrintCaseTableDirector([NotNull] IActionVisitMapper<INode, IPrintCaseTableDirector> visitMapper,
                                      [NotNull] ITableBuilder tableBuilder)
            : base(visitMapper)
        {
            if (tableBuilder == null) throw new ArgumentNullException("tableBuilder");
            mTableBuilder = tableBuilder;
        }

        public void NewRow()
        {
            mAmountOfCases++;
            mTableBuilder.NewRow();
            mTableBuilder.Print(sIndexClumn, mAmountOfCases.ToString());
        }

        public void Print([NotNull] CodeLocation codeLocation,
                          [NotNull] ITableColumn column,
                          [NotNull] string format,
                          [NotNull] params object[] args)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (column == null) throw new ArgumentNullException("column");
            if (format == null) throw new ArgumentNullException("format");
            if (args == null) throw new ArgumentNullException("args");

            mTableBuilder.Print(column, format, args);
        }

        [NotNull]
        public string GetString()
        {
            var sb = new StringBuilder();
            mTableBuilder.GenerateTable(sb);
            sb.AppendLine();
            sb.AppendFormat("TOTAL: {0} TEST CASES", mAmountOfCases);

            return sb.ToString();
        }
    }
}