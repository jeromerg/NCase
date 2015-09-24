using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Replay
{
    public interface IReplayVisitor<TNod> : IActionPayloadVisitor<INode, IReplayDirector, TNod, bool>
        where TNod : INode
    {
    }
}