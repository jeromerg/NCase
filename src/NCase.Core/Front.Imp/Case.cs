using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Replay;
using NCase.Front.Api;
using NDsl.Api.Core;


namespace NCase.Front.Imp
{
    public class Case : ICase
    {
        #region inner types

        public class Factory : ICaseFactory
        {
            private readonly IReplayDirector mReplayDirector;

            public Factory([NotNull] IReplayDirector replayDirector)
            {
                if (replayDirector == null) throw new ArgumentNullException("replayDirector");
                mReplayDirector = replayDirector;
            }

            public ICase Create(IEnumerable<INode> facts)
            {
                return new Case(facts, mReplayDirector);
            }
        }

        #endregion

        private readonly IReplayDirector mReplayDirector;
        private readonly IEnumerable<INode> mFactNodes;

        public Case(IEnumerable<INode> factNodes, IReplayDirector replayDirector)
        {
            mFactNodes = factNodes;
            mReplayDirector = replayDirector;
        }

        public void Replay(bool isReplay)
        {
            foreach (INode factNode in mFactNodes)
                mReplayDirector.Visit(factNode, isReplay);
        }
    }
}