using System.Text;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Api.Dev.Core.Print
{
    public class PrintDetailsDirector : ActionPayloadDirector<INode, IPrintDetailsDirector, StringBuilder>
    {
        public PrintDetailsDirector(IActionPayloadVisitMapper<INode, IPrintDetailsDirector, StringBuilder> visitMapper)
            : base(visitMapper)
        {
        }
    }
}