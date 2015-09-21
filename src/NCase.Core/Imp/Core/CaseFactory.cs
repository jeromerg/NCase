using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Replay;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class CaseFactory : ICaseFactory
    {
        private readonly IReplayDirector mReplayDirector;

        public CaseFactory([NotNull] IReplayDirector replayDirector)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            mReplayDirector = replayDirector;
        }

        public ICase Create(IEnumerable<INode> facts)
        {
            return new Case(facts, mReplayDirector);
        }
    }
}
