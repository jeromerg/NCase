using System.Text;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintCasePayload : IPrintCasePayload
    {
        private static readonly ITableColumn sFactColumn = new SimpleTableColumn("Fact");
        private static readonly ITableColumn sFileInfoColumn = new SimpleTableColumn("Location");

        private readonly ITableBuilder mTableBuilder;

        public class Factory : IPrintCasePayloadFactory
        {
            public IPrintCasePayload Create()
            {
                return new PrintCasePayload();
            }
        }

        public void PrintFact(CodeLocation codeLocation, string format, params object[] args)
        {
            mTableBuilder.NewRow();
            mTableBuilder.Print(sFactColumn, format, args);
            mTableBuilder.Print(sFileInfoColumn, codeLocation.GetFullInfoWithSameSyntaxAsStackTrace());
        }

        public string GetString()
        {
            var sb = new StringBuilder();
            mTableBuilder.GenerateTable(sb);
            return sb.ToString();
        }
    }
}