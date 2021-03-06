using JetBrains.Annotations;
using NCaseFramework.Back.Api.SetDef;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public interface ICombinationSetNode : ISetDefNode
    {
        [NotNull] new CombinationSetId Id { get; }

        [CanBeNull] IProdNode Product { get; set; }
        bool IsOnlyPairwise { get; }
    }
}