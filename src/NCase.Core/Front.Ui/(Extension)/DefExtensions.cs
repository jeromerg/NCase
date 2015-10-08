using NCase.Front.Api;
using NCase.Front.Imp.Op;

namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static ICaseEnumerable Cases<TSetDef>(this TSetDef setDef)
            where TSetDef : class, ISetDef<ISetDefApi>
        {
            return setDef.Api.Tool<IGetCases>().Perform(setDef.Api);
        }

        public static string PrintDefinition<TSetDef>(this TSetDef setDef, bool includeFileInfo = false, bool isRecursive = false)
            where TSetDef : class, ISetDef
        {
            return
                setDef.Perform<PrintDefinition, string>(new PrintDefinition
                                                        {
                                                            IncludeFileInfo = includeFileInfo,
                                                            IsRecursive = isRecursive
                                                        });
        }

        //public static string PrintTable<TSetDef>(this TSetDef setDef, bool isRecursive = false)
        //    where TSetDef : class, ISetDef<TSetDef>
        //{
        //    return setDef.Perform<PrintTable, string>(new PrintTable {IsRecursive = isRecursive});
        //}
    }
}