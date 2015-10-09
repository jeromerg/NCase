using NDsl.Back.Api.Common;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintCaseTableVisitor<TNod> : IActionVisitor<INode, IPrintCaseTableDirector, TNod>
        where TNod : INode
    {
    }
}