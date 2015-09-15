using System;
using NCase.Api.Dev;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Prod;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<ProdCaseSet>>
        , IParseVisitor<EndToken<ProdCaseSet>>
        , IParseVisitor<RefToken<ProdCaseSet>>
    {

        public void Visit(IParseDirector dir, BeginToken<ProdCaseSet> token)
        {
            var newCaseSetNode = new ProdNode(token.CodeLocation, token.Owner);

            dir.AddReference(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<ProdCaseSet> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<ProdCaseSet> token)
        {
            IProdNode referredSetNode = dir.GetReference<IProdNode>(token.Owner, token.CodeLocation);

            var newNode = new RefNode<IProdNode>(referredSetNode, token.CodeLocation);

            dir.AddChildToScope(newNode);
        }
    }
}