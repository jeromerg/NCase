using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Action;

namespace NCase.Imp.Core.Replay
{
    public class ReplayDirector : ActionDirector<INode, IReplayDirector>, IReplayDirector
    {
        public ReplayDirector(IActionVisitMapper<INode, IReplayDirector> visitMapper)
            : base(visitMapper)
        {
        }

        public bool IsReplay { get; set; }
    }
}