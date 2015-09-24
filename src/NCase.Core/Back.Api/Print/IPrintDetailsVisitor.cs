using System.Text;
using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Print
{
    public interface IPrintDetailsVisitor<TNod> : IActionPayloadVisitor<INode, IPrintDetailsDirector, TNod, StringBuilder>
        where TNod : INode
    {
    }
}