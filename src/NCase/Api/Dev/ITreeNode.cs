using System.Collections.Generic;
using NCase.Api;
using NCase.Imp.Core;
using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public interface ITreeNode : ICaseSetNode<ITree>
    {
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void PlaceNextNode([NotNull] INode child);
    }
}