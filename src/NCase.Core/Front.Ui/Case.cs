using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Api.Case;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Case : IArtefact<ICaseModel>
    {
    }
}