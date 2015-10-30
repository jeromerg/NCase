using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintCaseVisitors : IPrintCaseVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintCaseDirector director, IInterfaceRecPlayNode node, IPrintCasePayload payload)
        {
            string fact = node.PropertyValue != null ? node.PropertyValue.ToString() : "null";
            payload.PrintFact(node.CodeLocation, fact);
        }
    }
}