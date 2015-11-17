using System.Collections.Generic;
using NCaseFramework.Front.Api.Fact;

namespace NCaseFramework.Front.Ui
{
    public static class FactExtensions
    {
        public static IEnumerable<Fact> Replay(this IEnumerable<Fact> facts)
        {
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

        public static Fact Replay(this Fact fact, bool iReplay)
        {
            var replayFact = fact.Zapi.Services.GetService<IReplayFact>();
            replayFact.Perform(fact.Zapi.Model, iReplay);
            return fact;
        }
    }
}