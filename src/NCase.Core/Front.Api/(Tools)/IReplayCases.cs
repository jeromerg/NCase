using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;

namespace NCase.Front.Imp.Op
{
    public interface IReplayCases : ITool<ICaseEnumerableModel>
    {
        ICaseEnumerable Perform(ICaseEnumerableModel caseEnumerableModel);
    }
}