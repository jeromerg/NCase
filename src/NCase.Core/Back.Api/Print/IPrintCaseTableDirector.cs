using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Back.Api.Util.Table;
using NVisitor.Api.Action;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCaseTableDirector : IActionDirector<INode, IPrintCaseTableDirector>
    {
        void NewRow();

        [StringFormatMethod("args")]
        void Print([NotNull] CodeLocation codeLocation,
                   [NotNull] ITableColumn column,
                   [NotNull] string format,
                   [NotNull] params object[] args);

        [NotNull] string GetString();
    }
}