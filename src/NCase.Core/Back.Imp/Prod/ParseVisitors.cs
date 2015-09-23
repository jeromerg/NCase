using NCase.Back.Api.Parse;
using NCase.Back.Api.Prod;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Ref;

namespace NCase.Back.Imp.Prod
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<ProdId>>,
          IParseVisitor<EndToken<ProdId>>,
          IParseVisitor<RefToken<ProdId>>
    {
        public void Visit(IParseDirector dir, BeginToken<ProdId> token)
        {
            var defNode = new ProdNode(token.CodeLocation, token.OwnerId);
            dir.AddId(token.OwnerId, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<ProdId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<ProdId> token)
        {
            var referredSetNode = dir.GetReferencedNode<IProdNode>(token.OwnerId, token.CodeLocation);

            var newNode = new RefNode<IProdNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}