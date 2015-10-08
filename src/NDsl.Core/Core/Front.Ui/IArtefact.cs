using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IArtefact<out TApi>
        where TApi : IArtefactApi
    {
        [NotNull] TApi Api { get; }
    }
}