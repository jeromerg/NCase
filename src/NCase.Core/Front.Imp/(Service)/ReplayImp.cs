using System;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NCaseFramework.Front.Api.Fact;

namespace NCaseFramework.Front.Imp
{
    public class ReplayFactSvc : IReplayFactSvc
    {
        [NotNull] private readonly IReplayDirector mReplayDirector;

        public ReplayFactSvc([NotNull] IReplayDirector replayDirector)
        {
            mReplayDirector = replayDirector;
        }

        public void Perform([NotNull] IFactModel factModel, bool isReplay)
        {
            if (factModel == null) throw new ArgumentNullException("factModel");

            if (isReplay)
                factModel.Recorder.SetReadMode(true);

            mReplayDirector.Visit(factModel.FactNode, isReplay);

            if (!isReplay)
                factModel.Recorder.SetReadMode(false);
        }
    }
}