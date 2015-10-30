using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCaseVisitor<TNod> : IActionPayloadVisitor<INode, IPrintCaseDirector, TNod, IPrintCasePayload>
        where TNod : INode
    {
    }
}