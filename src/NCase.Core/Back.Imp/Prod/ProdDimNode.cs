using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Back.Imp.Prod
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class ProdDimNode : INode
    {
        [NotNull] private readonly INode mFirstChild;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();

        public ProdDimNode(INode firstChild)
        {
            mFirstChild = firstChild;
        }

        [NotNull] public INode FirstChild
        {
            get { return mFirstChild; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mFirstChild.CodeLocation; }
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