using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Back.Api.Core.Replay
{
    public interface IReplayDirector : IActionPayloadDirector<INode, IReplayDirector, bool>
    {
    }
}