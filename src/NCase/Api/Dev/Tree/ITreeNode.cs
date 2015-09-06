using System.Collections.Generic;
using NCase.Api.Dev.Core.CaseSet;
using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Api.Dev.Tree
{
    public interface ITreeNode : ICaseSetNode<ITree>
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void PlaceNextNode([NotNull] INode child);
    }
}