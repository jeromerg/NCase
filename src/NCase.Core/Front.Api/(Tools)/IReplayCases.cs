using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IReplayCases : ITool<ICaseEnumerableApi>
    {
        ICaseEnumerable Perform(ICaseEnumerableApi caseEnumerableApi);
    }
}