using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Back.Api.Parse;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<PairwiseCombinationsId>>,
          IParseVisitor<EndToken<PairwiseCombinationsId>>,
          IParseVisitor<RefToken<PairwiseCombinationsId>>
    {
        public void Visit(IParseDirector dir, BeginToken<PairwiseCombinationsId> token)
        {
            var defNode = new PairwiseNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<PairwiseCombinationsId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<PairwiseCombinationsId> token)
        {
            var referredSetNode = dir.GetNodeForId<IPairwiseNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}