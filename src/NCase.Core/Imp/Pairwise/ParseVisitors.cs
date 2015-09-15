using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;

namespace NCase.Imp.Pairwise
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<PairwiseCaseSet>>
        , IParseVisitor<EndToken<PairwiseCaseSet>>
        , IParseVisitor<RefToken<PairwiseCaseSet>>
    {

        public void Visit(IParseDirector dir, BeginToken<PairwiseCaseSet> token)
        {
            var newCaseSetNode = new PairwiseNode(token.CodeLocation, token.Owner);

            dir.AddReference(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<PairwiseCaseSet> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<PairwiseCaseSet> token)
        {
            IPairwiseNode referredSetNode = dir.GetReference<IPairwiseNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}