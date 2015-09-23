using NCase.Back.Api.Core.Replay;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Imp.Core.Replay
{
    public class ReplayDirector : ActionPayloadDirector<INode, IReplayDirector, bool>, IReplayDirector
    {
        public ReplayDirector(IActionPayloadVisitMapper<INode, IReplayDirector, bool> visitMapper)
            : base(visitMapper)
        {
        }
    }
}