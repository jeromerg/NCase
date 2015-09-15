using System.Collections.Generic;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Api.Dev.Tree
{
    public interface ITreeNode : ICaseSetNode
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void AddTreeBranch(INode branch);
    }
}