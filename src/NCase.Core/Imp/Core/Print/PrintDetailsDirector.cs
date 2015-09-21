using System.Text;
using NCase.Api.Dev.Core.Print;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Imp.Core.Print
{
    public class PrintDetailsDirector : ActionPayloadDirector<INode, IPrintDetailsDirector, StringBuilder>, IPrintDetailsDirector
    {
        public PrintDetailsDirector(IActionPayloadVisitMapper<INode, IPrintDetailsDirector, StringBuilder> visitMapper)
            : base(visitMapper)
        {
        }
    }
}