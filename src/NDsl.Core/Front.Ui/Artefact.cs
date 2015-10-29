using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NDsl.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Artefact<out TModel>
        where TModel : IArtefactModel
    {
        [NotNull] IApi<TModel> Api { get; }
    }
}