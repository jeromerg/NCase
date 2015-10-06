using NCase.Back.Api.Print;
using NCase.Back.Api.Prod;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Prod
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IProdNode>,
          IPrintDefinitionVisitor<ProdDimNode>
    {
        public void Visit(IPrintDefinitionDirector dir, IProdNode node)
        {
            dir.PrintLine(node.CodeLocation, "Prod '{0}'", node.Id.Name);

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }

        public void Visit(IPrintDefinitionDirector dir, ProdDimNode node)
        {
            dir.PrintLine(node.CodeLocation, "Implicit Seq");

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}