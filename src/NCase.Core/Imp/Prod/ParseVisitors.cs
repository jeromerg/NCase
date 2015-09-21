using NCase.Api;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Prod;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;

namespace NCase.Imp.Prod
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<IProd>>
        , IParseVisitor<EndToken<IProd>>
        , IParseVisitor<RefToken<IProd>>
    {

        public void Visit(IParseDirector dir, BeginToken<IProd> token)
        {
            var defNode = new ProdNode(token.CodeLocation, token.Owner);
            dir.AddReference(token.Owner, defNode);
            dir.PushScope(defNode);
        }

        public void Visit(IParseDirector dir, EndToken<IProd> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<IProd> token)
        {
            IProdNode referredSetNode = dir.GetReference<IProdNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IProdNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}