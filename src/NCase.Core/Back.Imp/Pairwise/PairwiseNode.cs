using System;
using System.Collections.Generic;
using NCase.Back.Api.Pairwise;
using NCase.Front.Api;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Back.Imp.Pairwise
{
    public class PairwiseNode : IPairwiseNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [CanBeNull] private readonly PairwiseId mPairwiseId;

        public PairwiseNode(
            [NotNull] ICodeLocation codeLocation,
            [CanBeNull] PairwiseId pairwiseId)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;

            mPairwiseId = pairwiseId;
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