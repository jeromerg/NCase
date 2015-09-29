using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Pairwise;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Pairwise
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class PairwiseDimNode : IPairwiseDimNode
    {
        [NotNull] private readonly INode mFirstChild;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();

        public PairwiseDimNode(INode firstChild)
        {
            mFirstChild = firstChild;
        }

        public ICodeLocation CodeLocation
        {
            get { return mFirstChild.CodeLocation; }
        }

        [NotNull] public INode FirstChild
        {
            get { return mFirstChild; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFirstChild;
                foreach (INode child in mChildren)
                    yield return child;
            }
        }

        public void PlaceNextValue(INode child)
        {
            mChildren.Add(child);
        }
    }
}