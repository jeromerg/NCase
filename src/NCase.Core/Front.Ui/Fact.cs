using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Api.Fact;
using NDsl.Front.Ui;

namespace NCaseFramework.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Fact : Artefact<IFactModel>
    {
    }
}