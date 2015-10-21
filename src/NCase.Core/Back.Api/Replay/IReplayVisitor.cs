using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Replay
{
    public interface IReplayVisitor<TNod> : IActionPayloadVisitor<INode, IReplayDirector, TNod, bool>
        where TNod : INode
    {
    }
}