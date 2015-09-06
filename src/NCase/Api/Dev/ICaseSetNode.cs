using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public interface ICaseSetNode : INode
    {
        void PlaceNextNode([NotNull] INode child);
    }

    public interface ICaseSetNode<out T> : ICaseSetNode
        where T : ICaseSet
    {
        [CanBeNull] T CaseSet { get; }
    }
}