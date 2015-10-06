namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases<TSetDef>(this TSetDef setDef)
            where TSetDef : class, ISetDef<TSetDef>
        {
            return setDef.Perform<GetCases, ICaseEnumerable>(GetCases.Default);
        }

        public static string PrintDefinition<TSetDef>(this TSetDef setDef, bool includeFileInfo = false, bool isRecursive = false)
            where TSetDef : class, ISetDef<TSetDef>
        {
            return setDef.Perform<PrintDefinition, string>(new PrintDefinition{IncludeFileInfo = includeFileInfo, IsRecursive = isRecursive});
        }

        public static string PrintTable<TSetDef>(this TSetDef setDef, bool isRecursive = true)
            where TSetDef : class, ISetDef<TSetDef>
        {
            return setDef.Perform<PrintTable, string>(new PrintTable {IsRecursive = isRecursive});
        }
    }
}