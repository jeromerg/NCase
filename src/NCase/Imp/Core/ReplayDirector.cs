using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core
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