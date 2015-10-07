using NCase.Front.Api;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Ui
{
    public interface ISetDef<TDefId, out TApi> : IDef<TDefId, TApi>
        where TApi : ISetDefApi<TDefId>
        where TDefId : IDefId
    {
    }
}