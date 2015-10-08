using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NDsl.All;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface ISetDefApi : IDefApi { }

    public interface ISetDefApi<out TApi, out TId> : IDefApi<TApi, TId>, ISetDefApi
        where TId : ISetDefId
        where TApi : ISetDefApi
    {
    }
}