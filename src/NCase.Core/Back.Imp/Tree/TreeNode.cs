using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NDsl.Api.Core;

namespace NCase.Back.Imp.Tree
{
    public class TreeNode : ITreeNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mBranches = new List<INode>();

        [CanBeNull] private readonly TreeId mId;
        [CanBeNull] private readonly INode mFact;

        public TreeNode([NotNull] ICodeLocation codeLocation,
                        [CanBeNull] TreeId id,
                        [CanBeNull] INode fact)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mId = id;
            mFact = fact;
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