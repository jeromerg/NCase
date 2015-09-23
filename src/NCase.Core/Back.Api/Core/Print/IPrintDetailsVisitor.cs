using System.Text;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Core.Print
{
    public interface IPrintDetailsVisitor<TNod> : IActionPayloadVisitor<INode, IPrintDetailsDirector, TNod, StringBuilder>
        where TNod : INode
    {
    }
}