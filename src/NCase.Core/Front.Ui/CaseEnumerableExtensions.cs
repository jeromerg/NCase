using NCaseFramework.Front.Api.CaseEnumerable;

namespace NCaseFramework.Front.Ui
{
    public static class CaseEnumerableExtensions
    {
        public static CaseEnumerable Replay(this CaseEnumerable caseEnumerable)
        {
            var replayCases = caseEnumerable.Zapi.Services.GetService<IReplayCases>();
            return replayCases.Perform(caseEnumerable.Zapi.Model);
        }
    }
}