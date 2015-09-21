using System;
using System.Collections.Generic;
using NCase.Api.Dev.Tree;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class TreeNode : ITreeNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mBranches = new List<INode>();

        [CanBeNull] private readonly ITree mDef;
        [CanBeNull] private readonly INode mFact;

        public TreeNode(
            [NotNull] ICodeLocation codeLocation,
            [CanBeNull] ITree def,
            [CanBeNull] INode fact)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mDef = def;
            mFact = fact;
        }

        public ITree Def
        {
            get { return mDef; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFact;
                foreach (INode caseBranchNode in Branches)
                    yield return caseBranchNode;
            }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public INode Fact
        {
            get { return mFact; }
        }

        public IEnumerable<INode> Branches
        {
            get { return mBranches; }
        }

        public void AddTreeBranch(INode branch)
        {
            mBranches.Add(branch);
        }
    }
}