using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Prod;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Prod
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IProdNode>,
          IPrintDefinitionVisitor<ProdDimNode>
    {
        public void Visit(IPrintDefinitionDirector dir, IProdNode node)
        {
            dir.PrintLine(node.CodeLocation, "AllCombinations '{0}'", node.Id.Name);

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }

        public void Visit(IPrintDefinitionDirector dir, ProdDimNode node)
        {
            dir.PrintLine(node.CodeLocation, "Implicit Dimension");

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}