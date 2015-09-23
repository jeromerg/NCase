using System.Collections.Generic;
using NCase.Back.Api.Core;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Back.Api.Tree
{
    public interface ITreeNode : IDefNode
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void AddTreeBranch(INode branch);
    }
}