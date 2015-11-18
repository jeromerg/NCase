using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintCaseDirector : IActionPayloadDirector<INode, IPrintCaseDirector, IPrintCasePayload>
    {
        [NotNull] IPrintCasePayload NewPayload();
    }
}