using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Pairwise;
using NCaseFramework.Front.Api.Pairwise;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface PairwiseCombinations : SetDefBase<IPairwiseCombinationsModel, PairwiseCombinationsId>
    {
    }
}