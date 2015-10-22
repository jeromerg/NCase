using JetBrains.Annotations;

namespace NDsl.Front.Api
{
    public interface Artefact<out TModel>
        where TModel : IArtefactModel
    {
        [NotNull] IApi<TModel> Api { get; }
    }
}