using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Pairwise
{
    public interface IPairwiseDimNode : INode
    {
        INode FirstChild { get; }
        void PlaceNextValue(INode child);
    }
}