using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Print
{
    public interface IPrintDefinitionDirector : IActionPayloadDirector<INode, IPrintDefinitionDirector, IPrintDefinitionPayload>
    {
    }
}