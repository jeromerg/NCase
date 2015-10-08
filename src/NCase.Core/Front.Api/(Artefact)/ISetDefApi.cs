using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface ISetDefApi : IDefApi
    {
        
    }

    public interface ISetDefApi<out TDefId> : IDefApi<TDefId>
        where TDefId : ISetDefId
    {
        
    }
}