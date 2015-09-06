using NDsl.Api.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Api.Dev.Core.CaseSet
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