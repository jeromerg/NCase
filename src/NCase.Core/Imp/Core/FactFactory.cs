using System;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class FactFactory : IFactFactory
    {
        private readonly IReplayDirector mReplayDirector;

        public FactFactory([NotNull] IReplayDirector replayDirector)
        {
            if (replayDirector == null) throw new ArgumentNullException("replayDirector");
            mReplayDirector = replayDirector;
        }

        public IFact Create(INode fact)
        {
            return new Fact(fact, mReplayDirector);
        }
    }
}
