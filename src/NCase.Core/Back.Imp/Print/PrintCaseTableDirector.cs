using System;
using System.Text;
using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Print
{
    public class PrintCaseTableDirector : ActionDirector<INode, IPrintCaseTableDirector>, IPrintCaseTableDirector
    {
        private static readonly SimpleTableColumn sIndexClumn = new SimpleTableColumn("#", HorizontalAlignment.Right);

        private readonly ITableBuilder mTableBuilder;
        private int mAmountOfCases;

        public PrintCaseTableDirector(IActionVisitMapper<INode, IPrintCaseTableDirector> visitMapper,
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

        public void Print(CodeLocation codeLocation, ITableColumn column, string format, params object[] args)
        {
            mTableBuilder.Print(column, format, args);
        }

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