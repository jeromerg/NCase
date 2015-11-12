using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NCaseFramework.Front.Api.CaseEnumerable;
using NCaseFramework.Front.Api.Fact;

namespace NCaseFramework.Front.Imp
{
    public class ReplayFact : IReplayFact
    {
        [NotNull] private readonly IReplayDirector mReplayDirector;

        public ReplayFact([NotNull] IReplayDirector replayDirector)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            mReplayDirector = replayDirector;
        }

        public void Perform(IFactModel factModel, bool iReplay)
        {
            mReplayDirector.Visit(factModel.FactNode, iReplay);
        }
    }
}