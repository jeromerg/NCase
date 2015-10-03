using NCase.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintDefinitionVisitors : IPrintDefinitionVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IPrintDefinitionDirector dir, IInterfaceRecPlayNode node)
        {
            dir.Print(node.PrintAssignment());
        }
    }
}