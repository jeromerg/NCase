using NCase.Back.Api.Replay;
using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Imp.Replay
{
    public class ReplayDirector : ActionPayloadDirector<INode, IReplayDirector, bool>, IReplayDirector
    {
        public ReplayDirector(IActionPayloadVisitMapper<INode, IReplayDirector, bool> visitMapper)
            : base(visitMapper)
        {
        }
    }
}