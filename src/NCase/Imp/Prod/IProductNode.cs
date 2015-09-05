using System.Collections.Generic;
using NCase.Api.Dev;
using NCase.Imp.Core;
using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public interface IProductNode : ICaseSetNode
    {
        [CanBeNull] ICaseSet CaseSet { get; }
        [CanBeNull] INode Fact { get; }

        IEnumerable<INode> Branches { get; }
        void PlaceNextNode([NotNull] INode child);
    }
}