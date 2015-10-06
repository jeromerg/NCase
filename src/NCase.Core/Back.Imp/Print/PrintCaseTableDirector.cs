using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.DictionaryAdapter;
using JetBrains.Annotations;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Print
{
    public class PrintCaseTableDirector : ActionDirector<INode, IPrintCaseTableDirector>, IPrintCaseTableDirector
    {
        private int mAmountOfCases = 0;
        private readonly ITableBuilder mTableBuilder;

        public PrintCaseTableDirector(IActionVisitMapper<INode, IPrintCaseTableDirector> visitMapper,
                                      [NotNull] ITableBuilder tableBuilder)
            : base(visitMapper)
        {
            if (tableBuilder == null) throw new ArgumentNullException("tableBuilder");
            mTableBuilder = tableBuilder;
        }

        public bool RecurseIntoReferences { get; set; }
        public bool IncludeFileInfo { get; set; }

        public void NewRow()
        {
            mAmountOfCases++;
            mTableBuilder.NewRow();
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