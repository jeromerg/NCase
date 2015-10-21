using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintDefinitionVisitors : IPrintDefinitionVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintDefinitionDirector dir, IInterfaceRecPlayNode node)
        {
            dir.PrintLine(node.CodeLocation, node.PrintAssignment());
        }
    }
}