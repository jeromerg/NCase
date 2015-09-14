using System;
using NCase.Api.Dev;
using NCase.Api.Dev.Core.CaseSet;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Tree;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Tok;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class ParseVisitors
        : IParseVisitor<BeginToken<TreeCaseSet>>
        , IParseVisitor<EndToken<TreeCaseSet>>
        , IParseVisitor<RefToken<TreeCaseSet>>
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
            var newCaseSetNode = new TreeNode(
                token.CodeLocation, 
                mGetBranchingKeyDirector, 
                token.Owner, 
                null /*root without associated fact*/);
            
            dir.AddReference(token.Owner, newCaseSetNode);
            dir.PushScope(newCaseSetNode);
        }

        public void Visit(IParseDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.PopScope();
        }

        public void Visit(IParseDirector dir, RefToken<TreeCaseSet> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            ICaseSetNode referredSetNode = dir.GetReference<ITreeNode>(token.Owner, codeLocation);

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.AddChildToScope(newNode);
        }
        #endregion
    }
}