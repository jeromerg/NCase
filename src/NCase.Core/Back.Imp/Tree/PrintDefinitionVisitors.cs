using NCase.Back.Api.Print;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Tree
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ITreeNode>,
          IPrintDefinitionVisitor<IRefNode<ITreeNode>>
    {
        public void Visit(IPrintDefinitionDirector dir, IRefNode<ITreeNode> node)
        {
            if (dir.RecurseIntoReferences)
                dir.Visit(node.Reference);
            else
                dir.Print(node.CodeLocation, "Ref to TREE {0}", node.Reference.Id.Name);
        }

        public void Visit(IPrintDefinitionDirector dir, ITreeNode node)
        {
            if (node.Fact == null)
                dir.Print(node.CodeLocation, "TREE {0}", node.Id.Name);
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