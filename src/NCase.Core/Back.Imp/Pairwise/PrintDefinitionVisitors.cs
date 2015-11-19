using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IPairwiseNode>,
          IPrintDefinitionVisitor<IPairwiseDimNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IPairwiseDimNode node, [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, "Implicit Dimension");

            payload.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child, payload);

            payload.Dedent();
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IPairwiseNode node, [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, "PairwiseCombinations '{0}'", node.Id.Name);

            payload.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child, payload);

            payload.Dedent();
        }
    }
}