using System.Collections.Generic;

namespace NCase.Front.Ui
{
    public static class DefExtensions
    {
        public static IEnumerable<ICase> Cases<TSetDef>(this TSetDef setDef)
            where TSetDef : class, ISetDef<TSetDef>
        {
            return setDef.Perform<GetCases, IEnumerable<ICase>>(GetCases.Instance);
        }
    }
}