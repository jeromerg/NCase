using NCase.Back.Api.Print;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Seq
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ISeqNode>
    {
        public void Visit(IPrintDefinitionDirector dir, ISeqNode node)
        {
            dir.Print(node.CodeLocation, "Seq '{0}'", node.Id.Name);

            dir.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child);

            dir.Dedent();
        }
    }
}