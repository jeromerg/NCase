using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Prod;
using NCaseFramework.Front.Api.Prod;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface AllCombinations : SetDefBase<IAllCombinationsModel, AllCombinationsId, Definer>
    {
    }
}