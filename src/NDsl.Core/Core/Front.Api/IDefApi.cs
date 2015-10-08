using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    public interface IDefApi : IArtefactApi
    {
        [NotNull] IDefId Id { get; }
        IBook Book { get; }
    }

    public interface IDefApi<out TDefId> : IArtefactApi, IDefApi
        where TDefId : IDefId
    {
        [NotNull] new TDefId Id { get; }
    }
}