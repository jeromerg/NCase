using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.SetDef;

namespace NCaseFramework.Front.Ui
{
    public static class CaseExtensions
    {
        [NotNull]
        public static string Print([NotNull] this Case cas)
        {
            if (cas == null) throw new ArgumentNullException("cas");

            var printCase = cas.Zapi.Services.GetService<IPrintCaseSvc>();
            return printCase.PrintCase(cas.Zapi.Model);
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<Case> Replay([NotNull, ItemNotNull] this IEnumerable<Case> cases)
        {
            if (cases == null) throw new ArgumentNullException("cases");

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

        [NotNull]
        public static Case Replay([NotNull] this Case @case, bool isReplay)
        {
            if (@case == null) throw new ArgumentNullException("case");

            foreach (Fact fact in @case.Facts)
                fact.Replay(isReplay);

            return @case;
        }
    }
}