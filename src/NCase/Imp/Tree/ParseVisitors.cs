using System;
using NCase.Api.Dev;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NVisitor.Api.Batch;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class ParseVisitors
        : IVisitor<IToken, IParseDirector, BeginToken<TreeCaseSet>>
        , IVisitor<IToken, IParseDirector, EndToken<TreeCaseSet>>
        , IVisitor<IToken, IParseDirector, RefToken<TreeCaseSet>>
    {
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        public ParseVisitors([NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
        {
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");
            mGetBranchingKeyDirector = getBranchingKeyDirector;
        }

        #region ITree related nodes
        public void Visit(IParseDirector dir, BeginToken<TreeCaseSet> token)
        {
            var newCaseSetNode = new CaseTreeNode(
                token.CodeLocation, 
                mGetBranchingKeyDirector, 
                token.Owner, 
                null /*root without associated fact*/);
            
            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentSetNode = newCaseSetNode;
        }

        public void Visit(IParseDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.CurrentSetNode = null;
        }

        public void Visit(IParseDirector dir, RefToken<TreeCaseSet> token)
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
        #endregion
    }
}