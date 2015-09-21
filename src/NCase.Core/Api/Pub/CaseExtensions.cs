using System.Collections.Generic;

namespace NCase.Api.Pub
{
    public static class CaseExtensions
    {
        public static IEnumerable<ICase> Replay(this IEnumerable<ICase> cases)
        {
            foreach (ICase cas in cases)
            {
                cas.Replay(true);
                yield return cas;
                cas.Replay(false);
            }
        } 
    }
}