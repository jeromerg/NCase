using NCase.Api.Vis;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.CaseSets
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