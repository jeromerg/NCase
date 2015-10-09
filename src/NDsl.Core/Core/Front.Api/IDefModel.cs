using JetBrains.Annotations;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    public interface IDefModel<out TId> : IArtefactModel
        where TId : IDefId
    {
        [NotNull] TId Id { get; }
        IBook Book { get; }
    }
}