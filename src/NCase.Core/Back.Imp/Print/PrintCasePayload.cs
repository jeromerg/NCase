using System;
using System.Text;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NDsl.Back.Imp.Util.Table;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintCasePayload : IPrintCasePayload
    {
        [NotNull] private static readonly ITableColumn sFactColumn = new SimpleTableColumn("Fact");
        [NotNull] private static readonly ITableColumn sFileInfoColumn = new SimpleTableColumn("Location");

        [NotNull] private readonly ITableBuilder mTableBuilder = new TableBuilder();

        public class Factory : IPrintCasePayloadFactory
        {
            [NotNull] 
            public IPrintCasePayload Create()
            {
                return new PrintCasePayload();
            }
        }

        public void PrintFact([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (format == null) throw new ArgumentNullException("format");
            if (args == null) throw new ArgumentNullException("args");

            mTableBuilder.NewRow();
            mTableBuilder.Print(sFactColumn, format, args);
            mTableBuilder.Print(sFileInfoColumn, codeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
        }

        [NotNull] 
        public string GetString()
        {
            var sb = new StringBuilder();
            mTableBuilder.GenerateTable(sb);
            return sb.ToString();
        }
    }
}