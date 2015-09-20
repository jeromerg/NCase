using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class PairwiseNode : IPairwiseNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mDimensions = new List<INode>();

        [CanBeNull] private readonly IPairwise mPairwise;

        public PairwiseNode(
            [NotNull] ICodeLocation codeLocation, 
            [CanBeNull] IPairwise pairwise)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            
            mPairwise = pairwise;
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

        public IPairwise CaseSet
        {
            get { return mPairwise; }
        }
    }
}