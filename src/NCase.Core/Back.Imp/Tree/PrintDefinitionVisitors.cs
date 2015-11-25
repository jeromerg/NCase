using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Tree;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Tree
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ITreeNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] ITreeNode node, [NotNull] IPrintDefinitionPayload payload)
        {
            if (node.CasesOfThisTreeNode == null)
                payload.PrintLine(node.CodeLocation, "Tree {0}", node.Id.Name);
            else
                dir.Visit(node.CasesOfThisTreeNode, payload);

            // ---

            payload.Indent();

            foreach (INode child in node.Branches)
                dir.Visit(child, payload);

            payload.Dedent();
        }
    }
}