using System.Collections.Generic;
using NCase.Api;
using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public interface ICaseTreeNode : INode
    {
        [CanBeNull] ICaseSet CaseSet { get; }
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void PlaceNextNode([NotNull] INode child);
    }
}