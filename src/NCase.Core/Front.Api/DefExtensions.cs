using System.Collections.Generic;

namespace NCase.Front.Api
{
    public static class DefExtensions
    {
        public static IEnumerable<ICase> Cases<TDef>(this TDef def)
            where TDef : class, IDef, IDef<TDef>
        {
            return def.Perform<GetCases, IEnumerable<ICase>>(GetCases.Instance);
        }
    }
}