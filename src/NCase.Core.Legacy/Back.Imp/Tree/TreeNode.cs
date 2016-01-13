using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Tree;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Tree
{
    public class TreeNode : ITreeNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<ITreeNode> mBranches = new List<ITreeNode>();
        [NotNull] private readonly TreeId mId;
        [CanBeNull] private readonly INode mCasesOfThisTreeNode;

        public TreeNode([NotNull] CodeLocation codeLocation,
                        [NotNull] TreeId id,
                        [CanBeNull] INode casesOfThisTreeNode)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (id == null) throw new ArgumentNullException("id");

            mCodeLocation = codeLocation;
            mId = id;
            mCasesOfThisTreeNode = casesOfThisTreeNode;
        }

        [NotNull] IDefId IDefNode.Id
        {
            get { return mId; }
        }

        [NotNull] public TreeId Id
        {
            get { return mId; }
        }

        [NotNull, ItemNotNull] public IEnumerable<INode> Children
        {
            get
            {
                yield return mCasesOfThisTreeNode ?? NullNode.Instance;
                foreach (INode caseBranchNode in Branches)
                    yield return caseBranchNode;
            }
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        [CanBeNull] public INode CasesOfThisTreeNode
        {
            get { return mCasesOfThisTreeNode; }
        }

        [NotNull, ItemNotNull] public IEnumerable<ITreeNode> Branches
        {
            get { return mBranches; }
        }

        public void AddTreeBranch([NotNull] ITreeNode branch)
        {
            mBranches.Add(branch);
        }
    }
}