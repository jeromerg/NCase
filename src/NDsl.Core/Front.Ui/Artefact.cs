using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NDsl.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface Artefact<out TModel>
    {
        [NotNull] IApi<TModel> Zapi { get; }
    }
}