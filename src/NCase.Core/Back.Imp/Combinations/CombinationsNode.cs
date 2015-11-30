using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Combinations;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Combinations
{
    public class CombinationsNode : ICombinationsNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<ICombinationsNode> mBranches = new List<ICombinationsNode>();

        [NotNull] private readonly CombinationsId mId;

        [CanBeNull] private readonly INode mCasesOfThisTreeNode;

        public CombinationsNode([NotNull] CodeLocation codeLocation,
                                [NotNull] CombinationsId id,
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

        [NotNull] public CombinationsId Id
        {
            get { return mId; }
        }

        [NotNull, ItemNotNull] public IEnumerable<INode> Children
        {
            get
            {
                yield return NullNode.Instance;
                foreach (ICombinationsNode caseBranchNode in Branches)
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

        [NotNull, ItemNotNull] public IEnumerable<ICombinationsNode> Branches
        {
            get { return mBranches; }
        }

        public void AddTreeBranch([NotNull] ICombinationsNode branch)
        {
            mBranches.Add(branch);
        }
    }
}