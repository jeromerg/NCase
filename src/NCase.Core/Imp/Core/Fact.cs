using System;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Replay;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class Fact : IFact
    {
        #region inner types

        public class Factory : IFactFactory
        {
            private readonly IReplayDirector mReplayDirector;

            public Factory([NotNull] IReplayDirector replayDirector)
            {
                if (replayDirector == null) throw new ArgumentNullException("replayDirector");
                mReplayDirector = replayDirector;
            }

            public IFact Create(INode fact)
            {
                return new Fact(fact, mReplayDirector);
            }
        }

        #endregion

        private readonly IReplayDirector mReplayDirector;
        private readonly INode mFactNode;

        public Fact(INode factNode, IReplayDirector replayDirector)
        {
            mFactNode = factNode;
            mReplayDirector = replayDirector;
        }

        public void Replay(bool isReplay)
        {
            mReplayDirector.Visit(mFactNode, isReplay);
        }
    }
}