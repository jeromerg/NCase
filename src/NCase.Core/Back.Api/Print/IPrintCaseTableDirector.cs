using JetBrains.Annotations;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Util;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintCaseTableDirector : IActionDirector<INode, IPrintCaseTableDirector>
    {
        bool RecurseIntoReferences { get; set; }
        bool IncludeFileInfo { get; set; }

        void NewRow();

        [StringFormatMethod("args")]
        void Print(CodeLocation codeLocation, ITableColumn column, string format, params object[] args);

        string GetString();
    }
}