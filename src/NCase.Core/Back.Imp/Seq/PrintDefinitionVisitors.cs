using JetBrains.Annotations;
using NCaseFramework.Back.Api.Print;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Seq
{
    public class PrintDefinitionVisitors
        : IPrintDefinitionVisitor<ISeqNode>
    {
        public void Visit([NotNull] IPrintDefinitionDirector dir,
                          [NotNull] ISeqNode node,
                          [NotNull] IPrintDefinitionPayload payload)
        {
            payload.PrintLine(node.CodeLocation, "Seq '{0}'", node.Id.Name);

            payload.Indent();

            foreach (INode child in node.Children)
                dir.Visit(child, payload);

            payload.Dedent();
        }
    }
}