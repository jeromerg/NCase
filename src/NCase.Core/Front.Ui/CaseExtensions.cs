namespace NCase.Front.Ui
{
    public static class CaseEnumerableExtensions
    {
        public static ICaseEnumerable Replay(this ICaseEnumerable caseEnumerable)
        {
            return caseEnumerable.Perform<Replay, ICaseEnumerable>(Ui.Replay.Default);
        }
    }
}