using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Prod;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Ref;

namespace NCaseFramework.Back.Imp.Prod
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<AllCombinationsId>>,
          IParseVisitor<EndToken<AllCombinationsId>>,
          IParseVisitor<RefToken<AllCombinationsId>>
    {
        public void Visit(IParseDirector dir, BeginToken<AllCombinationsId> token)
        {
            var defNode = new ProdNode(token.CodeLocation, token.Owner);
            dir.AddId(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<AllCombinationsId> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<AllCombinationsId> token)
        {
            var referredSetNode = dir.GetNodeForId<IProdNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IProdNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}