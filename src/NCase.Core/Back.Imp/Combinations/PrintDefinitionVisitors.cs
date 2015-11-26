using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Back.Api.Print;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ICombinationsNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] ICombinationsNode node,
                          [NotNull] IPrintDefinitionPayload payload)
        {
            if (node.CasesOfThisTreeNode == null)
                payload.PrintLine(node.CodeLocation, "Combinations {0}", node.Id.Name);
            else
                dir.Visit(node.CasesOfThisTreeNode, payload);

            // ---

            payload.Indent();

            foreach (ICombinationsNode child in node.Branches)
                dir.Visit(child, payload);

            payload.Dedent();
        }
    }
}