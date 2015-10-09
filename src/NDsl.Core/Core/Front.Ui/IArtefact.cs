using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IArtefact<out TModel>
        where TModel : IArtefactModel
    {
        [NotNull] IApi<TModel> Api { get; }
    }
}