using NCase.Front.Api;
using NCase.Front.Ui;

namespace NCase.Front.Imp.Op
{
    public interface IReplayCaseEnumerable
    {
        ICaseEnumerable Perform(ICaseEnumerableApi caseEnumerableApi);
    }
}