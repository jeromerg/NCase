using NCase.Back.Api.Core;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public interface IDefImp : IArtefactImp
    {
        IDefId DefId { get; }
        string DefName { get; }
        ITokenReaderWriter TokenReaderWriter { get; }
    }

    public interface IDefImp<TDefId> : IDefImp
        where TDefId : IDefId
    {
        TDefId Id { get; }
    }
}