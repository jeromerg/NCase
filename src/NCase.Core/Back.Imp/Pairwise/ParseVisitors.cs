using NCase.Back.Api.Core.Parse;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;

namespace NCase.Back.Imp.Pairwise
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<PairwiseId>>,
          IParseVisitor<EndToken<PairwiseId>>,
          IParseVisitor<RefToken<PairwiseId>>
    {
        public void Visit(IParseDirector dir, BeginToken<PairwiseId> token)
        {
            var defNode = new PairwiseNode(token.CodeLocation, token.OwnerId);
            dir.AddId(token.OwnerId, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<PairwiseId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<PairwiseId> token)
        {
            var referredSetNode = dir.GetReferencedNode<IPairwiseNode>(token.OwnerId, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}