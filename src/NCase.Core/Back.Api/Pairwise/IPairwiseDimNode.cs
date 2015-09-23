using NDsl.Api.Dev.Core.Nod;

namespace NCase.Back.Api.Pairwise
{
    public interface IPairwiseDimNode : INode
    {
        INode FirstChild { get; }
        void PlaceNextValue(INode child);
    }
}