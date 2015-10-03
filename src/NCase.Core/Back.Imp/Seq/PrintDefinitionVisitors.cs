using NCase.Back.Api.Print;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Seq
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ISeqNode>,
          IPrintDefinitionVisitor<IRefNode<ISeqNode>>
    {
        public void Visit(IPrintDefinitionDirector dir, IRefNode<ISeqNode> node)
        {
            if (dir.RecurseIntoReferences)
                dir.Visit(node.Reference);
            else
                dir.Print(node.CodeLocation, "Ref to SEQ {0}", node.Reference.Id.Name);
        }

        public void Visit(IPrintDefinitionDirector dir, ISeqNode node)
        {
            if (dir.IncludeFilePath)
                dir.Print(node.CodeLocation.GetFullInfo());

            dir.Print("SEQUENCE {0}{1}", node.Id.Name, node.CodeLocation.GetLineAndColumnInfo());

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}