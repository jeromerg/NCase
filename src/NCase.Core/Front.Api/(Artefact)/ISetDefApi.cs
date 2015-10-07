using NDsl.Back.Api.Core;
using NDsl.Front.Ui;

namespace NCase.Front.Api
{
    public interface ISetDefApi<out TDefId> : IDefApi<TDefId>
        where TDefId : IDefId
    {
        
    }
}