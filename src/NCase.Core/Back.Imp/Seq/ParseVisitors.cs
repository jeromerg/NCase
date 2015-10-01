using NCase.Back.Api.Parse;
using NCase.Back.Api.Seq;
using NDsl.Back.Api;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Seq
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
            ICodeLocation codeLocation = token.CodeLocation;

            IDefNode referredSetNode = dir.GetReferencedNode<ISeqNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ISeqNode>((ISeqNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}