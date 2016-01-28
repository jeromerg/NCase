using JetBrains.Annotations;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public interface IPairwiseProdNode : IProdNode
    {
        [NotNull] new PairwiseProdId Id { get; }
    }
}