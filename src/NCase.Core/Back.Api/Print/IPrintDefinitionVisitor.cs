using System.Text;
using NDsl.Back.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Print
{
    public interface IPrintDefinitionVisitor<TNod> : IActionPayloadVisitor<INode, IPrintDefinitionDirector, TNod, StringBuilder>
        where TNod : INode
    {
    }
}