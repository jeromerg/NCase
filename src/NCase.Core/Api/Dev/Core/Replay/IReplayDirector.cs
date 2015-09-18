using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayDirector : IActionPayloadDirector<INode, IReplayDirector, bool>
    {
    }
}