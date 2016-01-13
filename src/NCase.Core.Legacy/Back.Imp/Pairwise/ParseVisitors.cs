using JetBrains.Annotations;
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
        public void Visit([NotNull] IParseDirector dir, [NotNull] BeginToken<PairwiseCombinationsId> token)
        {
            var defNode = new PairwiseNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] EndToken<PairwiseCombinationsId> token)
        {
            dir.PopScope();
        }

        public void Visit([NotNull] IParseDirector dir, [NotNull] RefToken<PairwiseCombinationsId> token)
        {
            var referredSetNode = dir.GetNodeForId<IPairwiseNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}