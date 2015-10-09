using NCase.Front.Api.CaseEnumerable;

namespace NCase.Front.Ui
{
    public static class CaseEnumerableExtensions
    {
        public static ICaseEnumerable Replay(this ICaseEnumerable caseEnumerable)
        {
            var replayCases = caseEnumerable.Api.Services.GetTool<IReplayCases>();
            return replayCases.Perform(caseEnumerable.Api.Model);
        }
    }
}