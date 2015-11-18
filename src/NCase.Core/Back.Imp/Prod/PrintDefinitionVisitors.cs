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
        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] IProdNode node)
        {
            dir.PrintLine(node.CodeLocation, "AllCombinations '{0}'", node.Id.Name);

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }

        public void Visit([NotNull] IPrintDefinitionDirector dir, [NotNull] ProdDimNode node)
        {
            dir.PrintLine(node.CodeLocation, "Implicit Dimension");

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}