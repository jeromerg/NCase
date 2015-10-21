using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Tree;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Tree
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ITreeNode>
    {
        public void Visit(IPrintDefinitionDirector dir, ITreeNode node)
        {
            if (node.Fact == null)
                dir.PrintLine(node.CodeLocation, "Tree {0}", node.Id.Name);
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