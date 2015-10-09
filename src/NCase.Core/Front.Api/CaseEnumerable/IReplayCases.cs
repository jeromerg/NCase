using NCase.Front.Ui;
using NDsl.Back.Api.Util;

namespace NCase.Front.Api.CaseEnumerable
{
    public interface IReplayCases : IService<ICaseEnumerableModel>
    {
        ICaseEnumerable Perform(ICaseEnumerableModel caseEnumerableModel);
    }
}