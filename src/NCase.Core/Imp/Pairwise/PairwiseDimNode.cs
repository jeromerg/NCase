using System.Collections.Generic;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    /// <summary> Group together nodes having the same BranchingKey </summary>
    public class PairwiseDimNode : ICaseSetNode
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

        public void PlaceNextValue(INode child)
        {
            mChildren.Add(child);
        }
    }
}