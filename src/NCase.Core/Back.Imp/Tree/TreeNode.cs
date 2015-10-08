using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Tree
{
    public class TreeNode : ITreeNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mBranches = new List<INode>();

        [NotNull] private readonly TreeId mId;
        [CanBeNull] private readonly INode mFact;

        public TreeNode([NotNull] CodeLocation codeLocation,
                        [NotNull] TreeId id,
                        [CanBeNull] INode fact)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;

            mId = id;
            mFact = fact;
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public TreeId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFact ?? NullNode.Instance;
                foreach (INode caseBranchNode in Branches)
                    yield return caseBranchNode;
            }
        }

        public CodeLocation CodeLocation
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