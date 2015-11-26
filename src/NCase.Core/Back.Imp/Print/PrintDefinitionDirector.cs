using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Imp.Print
{
    public class PrintDefinitionDirector
        : ActionPayloadDirector<INode, IPrintDefinitionDirector, IPrintDefinitionPayload>, IPrintDefinitionDirector
    {
        public PrintDefinitionDirector(
            IActionPayloadVisitMapper<INode, IPrintDefinitionDirector, IPrintDefinitionPayload> visitMapper)
            : base(visitMapper)
        {
        }
    }
}