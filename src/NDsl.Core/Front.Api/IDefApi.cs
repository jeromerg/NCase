using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    public interface IDefApi<out TDefId> : IArtefactApi
        where TDefId : IDefId
    {
        [NotNull] TDefId Id { get; }
        IBook Book { get; }
    }
}