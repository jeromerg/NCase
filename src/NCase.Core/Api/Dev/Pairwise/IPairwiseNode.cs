using NDsl.Api.Dev.Core.Nod;

namespace NCase.Api.Dev.Pairwise
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used to produce the pairwise tests
    /// </summary>
    public interface IPairwiseNode : INode
    {
        void AddChild(INode child);
    }
}