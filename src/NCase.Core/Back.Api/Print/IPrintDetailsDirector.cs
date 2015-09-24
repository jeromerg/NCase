using System.Text;
using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Print
{
    public interface IPrintDetailsDirector : IActionPayloadDirector<INode, IPrintDetailsDirector, StringBuilder>
    {
    }
}