using NCase.Back.Api.SetDef;
using NDsl.Front.Api;

namespace NCase.Front.Api.SetDef
{
    public interface ISetDefModel<out TId> : IDefModel<TId>
        where TId : ISetDefId
    {
    }
}