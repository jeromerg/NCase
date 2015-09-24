using System.Text;
using NCase.Back.Api.Print;
using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Imp.Print
{
    public class PrintDetailsDirector : ActionPayloadDirector<INode, IPrintDetailsDirector, StringBuilder>, IPrintDetailsDirector
    {
        public PrintDetailsDirector(IActionPayloadVisitMapper<INode, IPrintDetailsDirector, StringBuilder> visitMapper)
            : base(visitMapper)
        {
        }
    }
}