using System.Diagnostics.CodeAnalysis;
using NDsl.Back.Api.Builder;
using NDsl.Front.Ui;

namespace NDsl.Front.Api
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface CaseBuilder : Artefact<ICaseBuilderModel>
    {
    }
}