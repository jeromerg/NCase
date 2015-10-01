using NDsl.Back.Api.Core;
using NVisitor.Api.Action;

namespace NCase.Back.Api.Print
{
    public interface IPrintDefinitionVisitor<TNod> : IActionVisitor<INode, IPrintDefinitionDirector, TNod>
        where TNod : INode
    {
    }
}