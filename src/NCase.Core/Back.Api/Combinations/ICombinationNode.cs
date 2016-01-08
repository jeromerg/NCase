using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.Combinations
{
    public interface ICombinationNode : ISetDefNode
    {
        [NotNull] new CombinationId Id { get; }

        [CanBeNull] IProdNode Product { get; set; }
        bool IsOnlyPairwise { get; }
    }
}