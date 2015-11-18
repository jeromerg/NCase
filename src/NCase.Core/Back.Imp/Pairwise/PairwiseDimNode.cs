using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Pairwise;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Pairwise
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class PairwiseDimNode : IPairwiseDimNode
    {
        [NotNull] private readonly INode mFirstChild;
        [NotNull, ItemNotNull] private readonly List<INode> mChildren = new List<INode>();

        public PairwiseDimNode([NotNull] INode firstChild)
        {
            if (firstChild == null) throw new ArgumentNullException("firstChild");

            mFirstChild = firstChild;
        }

        [NotNull]
        public CodeLocation CodeLocation
        {
            get { return mFirstChild.CodeLocation; }
        }

        [NotNull]
        public INode FirstChild
        {
            get { return mFirstChild; }
        }

        [NotNull, ItemNotNull]
        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFirstChild;
                foreach (INode child in mChildren)
                    yield return child;
            }
        }

        public void PlaceNextValue([NotNull] INode child)
        {
            if (child == null) throw new ArgumentNullException("child");

            mChildren.Add(child);
        }
    }
}