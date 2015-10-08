using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IArtefact
    {
        [NotNull] 
        IArtefactApi Api { get; }
    }
}