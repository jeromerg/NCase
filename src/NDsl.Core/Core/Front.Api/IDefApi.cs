using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    public interface IDefApi : IArtefactApi
    { }

    public interface IDefApi<out TApi, out TId> : IArtefactApi<TApi>, IDefApi
        where TId : IDefId
        where TApi : IDefApi
    {
        [NotNull] TId Id { get; }
        IBook Book { get; }
    }
}