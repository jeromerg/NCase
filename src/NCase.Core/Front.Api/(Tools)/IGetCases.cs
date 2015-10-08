using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IGetCases 
        : ITool<ISetDefApi<ISetDefApi, ISetDefId>>
    {
        ICaseEnumerable Perform(ISetDefApi<ISetDefApi, ISetDefId> setDefApi);
    }
}