using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Api
{
    public interface IDefImp
    {
        [NotNull] IDefId DefId { get; }
        IBook Book { get; }
    }
}