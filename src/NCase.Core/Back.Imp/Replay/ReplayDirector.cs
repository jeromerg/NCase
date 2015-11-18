using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Imp.Replay
{
    public class ReplayDirector : ActionPayloadDirector<INode, IReplayDirector, bool>, IReplayDirector
    {
        public ReplayDirector([NotNull] IActionPayloadVisitMapper<INode, IReplayDirector, bool> visitMapper)
            : base(visitMapper)
        {
        }
    }
}