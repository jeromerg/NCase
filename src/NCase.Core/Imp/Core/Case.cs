using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core.Replay;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Imp.Core
{
    public class Case : ICase
    {
        private readonly IReplayDirector mReplayDirector;
        private readonly IEnumerable<INode> mFactNodes;

        public Case(IEnumerable<INode> factNodes, IReplayDirector replayDirector)
        {
            mFactNodes = factNodes;
            mReplayDirector = replayDirector;
        }

        public void Replay(bool isReplay)
        {
            foreach (var factNode in mFactNodes)
                mReplayDirector.Visit(factNode, isReplay);
        }
    }
}
