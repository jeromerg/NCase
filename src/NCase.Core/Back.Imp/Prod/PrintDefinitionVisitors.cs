using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Prod;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Prod
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IProdNode>,
          IPrintDefinitionVisitor<ProdDimNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IProdNode node, [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, "AllCombinations '{0}'", node.Id.Name);

            payload.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child, payload);

            payload.Dedent();
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] ProdDimNode node, [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, "Implicit Dimension");

            payload.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child, payload);

            payload.Dedent();
        }
    }
}