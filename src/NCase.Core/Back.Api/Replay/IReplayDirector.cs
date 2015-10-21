using NDsl.Back.Api.Common;
using NVisitor.Api.ActionPayload;

namespace NCaseFramework.Back.Api.Replay
{
    public interface IReplayDirector : IActionPayloadDirector<INode, IReplayDirector, bool>
    {
    }
}