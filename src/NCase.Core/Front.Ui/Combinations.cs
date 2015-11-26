using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Front.Api.Combinations;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Combinations : SetDefBase<ICombinationsModel, CombinationsId, Definer>
    {
    }
}