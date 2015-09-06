using System.Collections.Generic;
using NCase.Imp.Core;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class ProdDimNode : ICaseSetNode
    {
        [NotNull] private readonly INode mFirstChild;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();

        public ProdDimNode(INode firstChild)
        {
            mFirstChild = firstChild;
        }

        public ICodeLocation CodeLocation
        {
            get { return mFirstChild.CodeLocation; }
        }

        [NotNull] 
        public INode FirstChild
        {
            get { return mFirstChild; }
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFirstChild;
                foreach (var child in mChildren)
                    yield return child;
            }
        }

        public void PlaceNextNode(INode child)
        {
            mChildren.Add(child);
        }
    }
}