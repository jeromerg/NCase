using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface IPairwiseProdNode : IProdNode
    {
        [NotNull] new PairwiseProdId Id { get; }
    }
}