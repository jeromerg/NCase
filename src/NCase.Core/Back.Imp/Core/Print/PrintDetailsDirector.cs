using System.Text;
using NCase.Back.Api.Core.Print;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Imp.Core.Print
{
    public class PrintDetailsDirector : ActionPayloadDirector<INode, IPrintDetailsDirector, StringBuilder>, IPrintDetailsDirector
    {
        public PrintDetailsDirector(IActionPayloadVisitMapper<INode, IPrintDetailsDirector, StringBuilder> visitMapper)
            : base(visitMapper)
        {
        }
    }
}