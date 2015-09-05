using System;
using NCase.Api.Dev;
using NCase.Imp.Tree;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ParseVisitors
        : IVisitor<IToken, IParseDirector, BeginToken<ProductCaseSet>>
        , IVisitor<IToken, IParseDirector, EndToken<ProductCaseSet>>
        , IVisitor<IToken, IParseDirector, RefToken<ProductCaseSet>>
    {
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        public ParseVisitors([NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
        {
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");
            mGetBranchingKeyDirector = getBranchingKeyDirector;
        }

        public void Visit(IParseDirector dir, BeginToken<ProductCaseSet> token)
        {
            var newCaseSetNode = new ProductNode(
                token.CodeLocation, 
                mGetBranchingKeyDirector, 
                token.Owner, 
                null /*root without associated fact*/);
            
            //dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            //dir.CurrentSetNode = newCaseSetNode;
        }

        public void Visit(IParseDirector dir, EndToken<ProductCaseSet> token)
        {
            dir.CurrentSetNode = null;
        }

        public void Visit(IParseDirector dir, RefToken<ProductCaseSet> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            if (dir.CurrentSetNode == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            ICaseTreeNode referredSetNode = dir.AllCaseSets[token.Owner];
            var newNode = new RefNode<ICaseTreeNode>(referredSetNode, codeLocation);

            dir.CurrentSetNode.PlaceNextNode(newNode);
        }
    }
}