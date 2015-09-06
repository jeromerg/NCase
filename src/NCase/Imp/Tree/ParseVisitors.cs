using System;
using NCase.Api.Dev;
using NCase.Imp.Core;
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
            var newCaseSetNode = new TreeNode(
                token.CodeLocation, 
                mGetBranchingKeyDirector, 
                token.Owner, 
                null /*root without associated fact*/);
            
            dir.AllCaseSets.Add(token.Owner, newCaseSetNode);
            dir.CurrentCaseSet = newCaseSetNode;
        }

        public void Visit(IParseDirector dir, EndToken<TreeCaseSet> token)
        {
            dir.CurrentCaseSet = null;
        }

        public void Visit(IParseDirector dir, RefToken<TreeCaseSet> token)
        {
            ICodeLocation codeLocation = token.CodeLocation;

            if (dir.CurrentCaseSet == null)
            {
                throw new InvalidSyntaxException("Call must be performed within CaseSet definition block: {0}",
                    codeLocation.GetUserCodeInfo());
            }

            ICaseSetNode<ICaseSet> referredSetNode;
            if (!dir.AllCaseSets.TryGetValue(token.Owner, out referredSetNode))
                throw new InvalidSyntaxException("Trying to reference a ITree that has not been defined yet: {0}", token);

            if (!(referredSetNode is ITreeNode))
                throw new ArgumentException("RefToken<TreeCaseSet> does not reference a ITreeNode. It should never happen");

            var newNode = new RefNode<ITreeNode>((ITreeNode) referredSetNode, codeLocation);

            dir.CurrentCaseSet.PlaceNextNode(newNode);
        }
        #endregion
    }
}