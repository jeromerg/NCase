using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Seq;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Seq
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<SeqId>>,
          IParseVisitor<EndToken<SeqId>>,
          IParseVisitor<RefToken<SeqId>>
    {
        public void Visit(IParseDirector dir, BeginToken<SeqId> token)
        {
            var newCaseSetNode = new SeqNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<SeqId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<SeqId> token)
        {
            CodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetReferencedNode<ISeqNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ISeqNode>((ISeqNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}