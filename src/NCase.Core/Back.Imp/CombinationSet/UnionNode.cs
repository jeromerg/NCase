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
    public class UnionNode : IUnionNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly UnionId mId;
        [NotNull] private readonly List<IBranchNode> mBranches = new List<IBranchNode>();

        public UnionNode([NotNull] UnionId id, [NotNull] CodeLocation codeLocation)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;
            mId = id;
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public UnionId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mBranches; }
        }

        public IEnumerable<IBranchNode> Branches
        {
            get { return mBranches; }
        }

        public void AddBranch(IBranchNode union)
        {
            mBranches.Add(union);
        }
    }
}