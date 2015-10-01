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
            if (dir.IncludeFilePath)
                dir.Print(node.CodeLocation.GetFullInfo());

            dir.Print("CARTESIAN PRODUCT {0}{1}", node.Id.Name, node.CodeLocation.GetLineAndColumnInfo());

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }

        public void Visit(IPrintDefinitionDirector dir, ProdDimNode node)
        {
            dir.Print("IMPLICIT SET");

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}