using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Action;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayVisitor<TNod> : IActionVisitor<INode, IReplayDirector, TNod> 
        where TNod : INode
    {
    }
}