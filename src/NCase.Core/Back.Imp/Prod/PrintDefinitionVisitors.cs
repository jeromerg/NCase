using NCase.Back.Api.Print;
using NCase.Back.Api.Prod;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Prod
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<IProdNode>,
          IPrintDefinitionVisitor<ProdDimNode>,
          IPrintDefinitionVisitor<IRefNode<IProdNode>>
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

        public void Visit(IPrintDefinitionDirector dir, IRefNode<IProdNode> node)
        {
            if (dir.RecurseIntoReferences)
                dir.Visit(node.Reference);
            else
                dir.Print(node.CodeLocation, "Ref to PROD {0}", node.Reference.Id.Name);
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