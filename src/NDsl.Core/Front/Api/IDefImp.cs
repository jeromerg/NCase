using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Api
{
    public interface IDefImp : IArtefactImp
    {
        [NotNull] IDefId DefId { get; }
        ITokenReaderWriter TokenReaderWriter { get; }
    }

    public interface IDefImp<TDefId> : IDefImp
        where TDefId : IDefId
    {
        [NotNull] TDefId Id { get; }
    }
}