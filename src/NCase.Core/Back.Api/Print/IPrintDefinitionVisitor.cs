using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintDefinitionVisitor<TNod>
        : IActionPayloadVisitor<INode, IPrintDefinitionDirector, TNod, IPrintDefinitionPayload>
        where TNod : INode
    {
    }
}