using System;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Print;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IPairwiseNode>,
          IPrintDefinitionVisitor<IPairwiseDimNode>
    {
        public void Visit(IPrintDefinitionDirector dir, IPairwiseDimNode node)
        {
            dir.PrintLine(node.CodeLocation, "Implicit Dimension");

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }

        public void Visit(IPrintDefinitionDirector dir, IPairwiseNode node)
        {
            dir.PrintLine(node.CodeLocation, "PairwiseCombinations '{0}'", node.Id.Name);

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}