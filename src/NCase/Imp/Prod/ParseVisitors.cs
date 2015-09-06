using System;
using NCase.Api.Dev;
using NCase.Imp.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ParseVisitors
        : IVisitor<IToken, IParseDirector, BeginToken<ProdCaseSet>>
        , IVisitor<IToken, IParseDirector, EndToken<ProdCaseSet>>
        , IVisitor<IToken, IParseDirector, RefToken<ProdCaseSet>>
    {
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        public ParseVisitors([NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
        {
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");
            mGetBranchingKeyDirector = getBranchingKeyDirector;
        }

        public void Visit(IParseDirector dir, BeginToken<ProdCaseSet> token)
        {
            var newCaseSetNode = new ProdNode(
                token.CodeLocation, 
                mGetBranchingKeyDirector, 
                token.Owner);

            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentCaseSet = newCaseSetNode;
        }

        public void Visit(IParseDirector dir, EndToken<ProdCaseSet> token)
        {
            dir.CurrentCaseSet = null;
        }

        public void Visit(IParseDirector dir, RefToken<ProdCaseSet> token)
        {
            if (dir.CurrentCaseSet == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    token.CodeLocation.GetUserCodeInfo());
            }

            ICaseSetNode<ICaseSet> referredSetNode;
            if(!dir.AllCaseSets.TryGetValue(token.Owner, out referredSetNode))
                throw new InvalidSyntaxException("Trying to reference a IProd that has not been defined yet: {0}", token);

            if(! (referredSetNode is IProdNode))
                throw new ArgumentException("RefToken<ProdCaseSet> does not reference a IProdNode. It should never happen");

            var newNode = new RefNode<IProdNode>((IProdNode)referredSetNode, token.CodeLocation);

            dir.CurrentCaseSet.PlaceNextNode(newNode);
        }
    }
}