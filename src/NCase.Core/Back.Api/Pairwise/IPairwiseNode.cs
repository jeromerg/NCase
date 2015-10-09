using JetBrains.Annotations;
using NCase.Back.Api.SetDef;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Common;

namespace NCase.Back.Api.Pairwise
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used to produce the pairwise tests
    /// </summary>
    public interface IPairwiseNode : ISetDefNode
    {
        [NotNull] new PairwiseId Id { get; }
        void AddChild(INode child);
    }
}