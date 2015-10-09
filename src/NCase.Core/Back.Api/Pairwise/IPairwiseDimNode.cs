using NDsl.Back.Api.Common;

namespace NCase.Back.Api.Pairwise
{
    public interface IPairwiseDimNode : INode
    {
        INode FirstChild { get; }
        void PlaceNextValue(INode child);
    }
}