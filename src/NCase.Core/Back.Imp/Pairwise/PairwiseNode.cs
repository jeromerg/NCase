using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Pairwise
{
    public class PairwiseNode : IPairwiseNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [NotNull] private readonly PairwiseCombinationsId mId;

        public PairwiseNode([NotNull] CodeLocation codeLocation, [NotNull] PairwiseCombinationsId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            mId = id;
        }

        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        public PairwiseCombinationsId Id
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mDimensions; }
        }

        public void AddChild(INode child)
        {
            mDimensions.Add(child);
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}