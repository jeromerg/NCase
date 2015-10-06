using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Pairwise
{
    public class PairwiseNode : IPairwiseNode
    {
        [NotNull] private readonly CodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [NotNull] private readonly PairwiseId mId;

        public PairwiseNode([NotNull] CodeLocation codeLocation, [NotNull] PairwiseId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            mId = id;
        }

        public IDefId DefId
        {
            get { return mId; }
        }

        public PairwiseId Id
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