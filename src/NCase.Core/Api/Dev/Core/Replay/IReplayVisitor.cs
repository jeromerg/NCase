using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.ActionPayload;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayVisitor<TNod> : IActionPayloadVisitor<INode, IReplayDirector, TNod, bool> 
        where TNod : INode
    {
    }
}