using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseVisitors : IPrintCaseVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintCaseDirector director, IInterfaceRecPlayNode node, IPrintCasePayload payload)
        {
            payload.PrintFact(node.CodeLocation, node.PrintAssignment());
        }
    }
}