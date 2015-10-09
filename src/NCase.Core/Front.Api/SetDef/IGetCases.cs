using NCase.Back.Api.SetDef;
using NCase.Front.Ui;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.SetDef
{
    public interface IGetCases 
        : IService<ISetDefModel<ISetDefId>>
    {
        ICaseEnumerable Perform(ISetDefModel<ISetDefId> setDefModel);
    }
}