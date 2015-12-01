using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Back.Api.Combinations;
using NCaseFramework.Front.Api.Combinations;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Combinations : SetDefBase<ICombinationsModel, CombinationsId, CombinationsDefiner>
    {
    }
}