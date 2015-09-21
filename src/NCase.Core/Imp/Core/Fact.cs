using NCase.Api.Dev.Core.Replay;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Imp.Core
{
    public class Fact : IFact
    {
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