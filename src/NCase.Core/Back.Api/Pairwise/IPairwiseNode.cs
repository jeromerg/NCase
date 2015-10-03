using JetBrains.Annotations;
using NDsl.Back.Api;
using NDsl.Back.Api.Core;

namespace NCase.Back.Api.Pairwise
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used to produce the pairwise tests
    /// </summary>
    public interface IPairwiseNode : IDefNode
    {
        [NotNull] PairwiseId Id { get; }
        void AddChild(INode child);
    }
}