namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases<TSetDef>(this TSetDef setDef)
            where TSetDef : class, ISetDef<TSetDef>
        {
            return setDef.Perform<GetCases, ICaseEnumerable>(GetCases.Default);
        }
    }
}