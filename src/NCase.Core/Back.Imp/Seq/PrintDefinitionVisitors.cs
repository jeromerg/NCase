using NCase.Back.Api.Print;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Seq
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ISeqNode>
    {
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