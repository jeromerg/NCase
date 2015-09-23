using NCase.Back.Api.Core;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Pairwise
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used to produce the pairwise tests
    /// </summary>
    public interface IPairwiseNode : IDefNode
    {
        void AddChild(INode child);
    }
}