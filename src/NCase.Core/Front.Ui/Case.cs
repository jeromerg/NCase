using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Api.Case;
using NDsl.Front.Api;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Case : Artefact<ICaseModel>
    {
    }
}