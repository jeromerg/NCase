using JetBrains.Annotations;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Pairwise
{
    public interface IPairwiseDimNode : INode
    {
        [NotNull] INode FirstChild { get; }
        void PlaceNextValue([NotNull] INode child);
    }
}