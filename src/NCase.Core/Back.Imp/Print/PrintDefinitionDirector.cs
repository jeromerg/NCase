using System.Text;
using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Imp.Print
{
    public class PrintDefinitionDirector : ActionPayloadDirector<INode, IPrintDefinitionDirector, StringBuilder>, IPrintDefinitionDirector
    {
        public PrintDefinitionDirector(IActionPayloadVisitMapper<INode, IPrintDefinitionDirector, StringBuilder> visitMapper)
            : base(visitMapper)
        {
        }
    }
}