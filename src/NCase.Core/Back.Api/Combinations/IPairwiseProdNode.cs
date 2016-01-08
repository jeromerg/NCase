using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface IPairwiseProdNode : ISetDefNode
    {
        [NotNull] new PairwiseProdId Id { get; }
    }
}