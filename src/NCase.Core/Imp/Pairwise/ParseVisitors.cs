using NCase.Api;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;

namespace NCase.Imp.Pairwise
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<IPairwise>>
        , IParseVisitor<EndToken<IPairwise>>
        , IParseVisitor<RefToken<IPairwise>>
    {

        public void Visit(IParseDirector dir, BeginToken<IPairwise> token)
        {
            var defNode = new PairwiseNode(token.CodeLocation, token.Owner);
            dir.AddReference(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<IPairwise> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<IPairwise> token)
        {
            IPairwiseNode referredSetNode = dir.GetReference<IPairwiseNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IPairwiseNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}