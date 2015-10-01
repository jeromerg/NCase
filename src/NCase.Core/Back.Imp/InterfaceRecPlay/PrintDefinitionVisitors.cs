using NCase.Back.Api.Print;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.InterfaceRecPlay
{
    public class PrintDefinitionVisitors : IPrintDefinitionVisitor<INode>
    {
        /// <summary> If node unknown, then recurse...</summary>
        public void Visit(IPrintDefinitionDirector dir, INode node)
        {
            foreach (INode child in node.Children)
                dir.Visit(child);
        }
    }
}