using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IArtefact<out TApi>
    {
        [NotNull] TApi Api { get; }
    }
}