using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintCaseTableDirector : IActionDirector<INode, IPrintCaseTableDirector>
    {
        void NewRow();

        [StringFormatMethod("args")]
        void Print(CodeLocation codeLocation, ITableColumn column, string format, params object[] args);

        string GetString();
    }
}