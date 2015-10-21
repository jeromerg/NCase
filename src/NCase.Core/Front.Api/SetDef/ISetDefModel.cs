using NCaseFramework.Back.Api.SetDef;
using NDsl.Front.Api;

namespace NCaseFramework.Front.Api.SetDef
{
    public interface ISetDefModel<out TId> : IDefModel<TId>
        where TId : ISetDefId
    {
    }
}