using NCase.Back.Api.Pairwise;
using NCase.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Pairwise
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<PairwiseId>>,
          IParseVisitor<EndToken<PairwiseId>>,
          IParseVisitor<RefToken<PairwiseId>>
    {
        public void Visit(IParseDirector dir, BeginToken<PairwiseId> token)
        {
            var defNode = new PairwiseNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<PairwiseId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<PairwiseId> token)
        {
            var referredSetNode = dir.GetReferencedNode<IPairwiseNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}