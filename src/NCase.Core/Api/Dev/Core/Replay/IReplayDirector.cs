using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Action;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayDirector : IActionDirector<INode, IReplayDirector>
    {
        bool IsReplay { get; set; }
    }
}