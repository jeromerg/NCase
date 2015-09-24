using NDsl.Api.Core;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Replay
{
    public interface IReplayDirector : IActionPayloadDirector<INode, IReplayDirector, bool>
    {
    }
}