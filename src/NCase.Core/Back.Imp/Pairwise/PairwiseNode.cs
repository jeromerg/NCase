using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Back.Api.Pairwise;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Pairwise
{
    public class PairwiseNode : IPairwiseNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [CanBeNull] private readonly PairwiseId mPairwiseId;

        public PairwiseNode([NotNull] ICodeLocation codeLocation, [CanBeNull] PairwiseId pairwiseId)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            mPairwiseId = pairwiseId;
        }

        public IDefId DefId
        {
            get { return mPairwiseId; }
        }

        public PairwiseId PairwiseId
        {
            get { return mPairwiseId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mDimensions; }
        }

        public void AddChild(INode child)
        {
            mDimensions.Add(child);
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }
    }
}