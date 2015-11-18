using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Fact;

namespace NCaseFramework.Front.Ui
{
    public static class FactExtensions
    {
        [NotNull, ItemNotNull]
        public static IEnumerable<Fact> Replay([NotNull, ItemNotNull] this IEnumerable<Fact> facts)
        {
            if (facts == null) throw new ArgumentNullException("facts");

            foreach (Fact fact in facts)
            {
                try
                {
                    fact.Replay(true);

                    yield return fact;
                }
                finally
                {
                    fact.Replay(false);
                }
            }
        }

        [NotNull]
        public static Fact Replay([NotNull] this Fact fact, bool iReplay)
        {
            if (fact == null) throw new ArgumentNullException("fact");

            var replayFact = fact.Zapi.Services.GetService<IReplayFactSvc>();
            replayFact.Perform(fact.Zapi.Model, iReplay);
            return fact;
        }
    }
}