using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    public static class CaseEnumerableExtensions
    {
        public static ICaseEnumerable Replay(this ICaseEnumerable caseEnumerable)
        {
            var replayCases = caseEnumerable.Api.Toolbox<IReplayCases>();
            return replayCases.Perform(caseEnumerable.Api);
        }
    }
}