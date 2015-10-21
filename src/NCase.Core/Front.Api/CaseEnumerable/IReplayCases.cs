using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Front.Api.CaseEnumerable
{
    public interface IReplayCases : IService<ICaseEnumerableModel>
    {
        Ui.CaseEnumerable Perform(ICaseEnumerableModel caseEnumerableModel);
    }
}