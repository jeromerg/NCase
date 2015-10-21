using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Prod;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Prod
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<ProdId>>,
          IParseVisitor<EndToken<ProdId>>,
          IParseVisitor<RefToken<ProdId>>
    {
        public void Visit(IParseDirector dir, BeginToken<ProdId> token)
        {
            var defNode = new ProdNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<ProdId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<ProdId> token)
        {
            var referredSetNode = dir.GetReferencedNode<IProdNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IProdNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}