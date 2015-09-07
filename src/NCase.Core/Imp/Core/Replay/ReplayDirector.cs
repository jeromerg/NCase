using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core.Replay
{
    public class ReplayDirector : Director<INode, IReplayDirector>, IReplayDirector
    {
        public ReplayDirector(IVisitMapper<INode, IReplayDirector> visitMapper)
            : base(visitMapper)
        {
        }

        public bool IsReplay { get; set; }
    }
}