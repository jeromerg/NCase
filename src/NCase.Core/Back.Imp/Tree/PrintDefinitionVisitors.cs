using NCase.Back.Api.Print;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Tree
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ITreeNode>
    {
        public void Visit(IPrintDefinitionDirector dir, ITreeNode node)
        {
            if (node.Fact == null)
                dir.PrintLine(node.CodeLocation, "TREE {0}", node.Id.Name);
            else
                dir.Visit(node.Fact);

            // ---

            dir.Indent();

            foreach (INode child in node.Branches)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}