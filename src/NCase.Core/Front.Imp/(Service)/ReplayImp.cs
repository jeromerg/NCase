﻿using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
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

        public void Perform(IFactModel factModel, bool isReplay)
        {
            if (isReplay)
                factModel.Recorder.SetReadMode(true);

            mReplayDirector.Visit(factModel.FactNode, isReplay);

            if (!isReplay)
                factModel.Recorder.SetReadMode(false);
        }
    }
}