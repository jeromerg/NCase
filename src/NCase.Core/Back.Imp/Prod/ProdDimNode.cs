using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Prod
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class ProdDimNode : INode
    {
        [NotNull] private readonly INode mFirstChild;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();

        public ProdDimNode([NotNull] INode firstChild)
        {
            if (firstChild == null) throw new ArgumentNullException("firstChild");

            mFirstChild = firstChild;
        }

        [NotNull] public INode FirstChild
        {
            get { return mFirstChild; }
        }

        [NotNull] 
        public CodeLocation CodeLocation
        {
            get { return mFirstChild.CodeLocation; }
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
            mChildren.Add(child);
        }
    }
}