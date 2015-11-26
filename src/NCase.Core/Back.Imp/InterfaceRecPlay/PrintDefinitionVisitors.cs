using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.InterfaceRecPlay
{
    public class PrintDefinitionVisitors : IPrintDefinitionVisitor<IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] IInterfaceRecPlayNode node,
                          [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, node.PrintAssignment());
        }
    }
}