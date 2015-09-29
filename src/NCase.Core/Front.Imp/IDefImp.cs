using NCase.Back.Api.Core;

namespace NCase.Front.Imp.Op
{
    public interface IDefImp : IArtefactImp
    {
        IDefId DefId { get; }
    }

    public interface IDefImp<TDefId> : IDefImp
        where TDefId : IDefId
    {
        TDefId Id { get; }
    }
}