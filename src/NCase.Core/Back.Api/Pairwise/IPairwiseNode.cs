using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Api.Pairwise
{
    /// <summary>
    ///     A child corresponds to a set of cases, which will be used to produce the pairwise tests
    /// </summary>
    public interface IPairwiseNode : ISetDefNode
    {
        [NotNull] new PairwiseCombinationsId Id { get; }
        void AddChild([NotNull] INode child);
    }
}