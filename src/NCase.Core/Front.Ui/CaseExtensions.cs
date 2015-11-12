using System.Collections.Generic;
using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class CaseExtensions
    {
        public static string Print(this Case cas)
        {
            var printCase = cas.Zapi.Services.GetService<IPrintCase>();
            return printCase.Perform(cas.Zapi.Model);
        }

        public static IEnumerable<Case> Replay(this IEnumerable<Case> cases)
        {
            foreach (Case @case in cases)
            {
                try
                {
                    Replay(@case, true);

                    yield return @case;
                }
                finally
                {
                    Replay(@case, false);
                }
            }
        }

        public static Case Replay(this Case @case, bool isReplay)
        {
            foreach (Fact fact in @case.Facts)
                fact.Replay(isReplay);

            return @case;
        }
    }
}