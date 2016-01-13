using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Seq
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<SequenceId>>,
          IParseVisitor<EndToken<SequenceId>>,
          IParseVisitor<RefToken<SequenceId>>
    {
        public void Visit([NotNull] IParseDirector dir, [NotNull] BeginToken<SequenceId> token)
        {
            var newCaseSetNode = new SeqNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<SequenceId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<SequenceId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetNodeForId<ISeqNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ISeqNode>((ISeqNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}