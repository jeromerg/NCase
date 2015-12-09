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
        [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

        [NotNull] private static readonly ITableColumn sFactColumn = new SimpleTableColumn("Fact");
        [NotNull] private static readonly ITableColumn sFileInfoColumn = new SimpleTableColumn("Location");

        [NotNull] private readonly ITableBuilder mTableBuilder = new TableBuilder();

        public PrintCasePayload([NotNull] ICodeLocationPrinter codeLocationPrinter)
        {
            mCodeLocationPrinter = codeLocationPrinter;
        }

        public class Factory : IPrintCasePayloadFactory
        {
                [NotNull] private readonly ICodeLocationPrinter mCodeLocationPrinter;

            public Factory([NotNull] ICodeLocationPrinter codeLocationPrinter)
            {
                mCodeLocationPrinter = codeLocationPrinter;
            }

            [NotNull]
            public IPrintCasePayload Create()
            {
                return new PrintCasePayload(mCodeLocationPrinter);
            }
        }

        public void PrintFact([NotNull] CodeLocation codeLocation, [NotNull] string format, [NotNull] params object[] args)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (format == null) throw new ArgumentNullException("format");
            if (args == null) throw new ArgumentNullException("args");

            mTableBuilder.NewRow();
            mTableBuilder.Print(sFactColumn, format, args);
            mTableBuilder.Print(sFileInfoColumn, mCodeLocationPrinter.Print(codeLocation));
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