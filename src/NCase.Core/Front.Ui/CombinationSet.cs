using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.CombinationSet;
using NCaseFramework.Front.Api.CombinationSet;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface CombinationSet : SetDefBase<ICombinationSetModel, CombinationSetId, CombinationSetDefiner>
    {
        bool IsOnlyPairwiseProduct { get; set; }
    }
}