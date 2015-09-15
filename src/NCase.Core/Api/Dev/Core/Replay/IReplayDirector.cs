using NDsl.Api.Dev.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayDirector : IDirector<INode, IReplayDirector>
    {
        bool IsReplay { get; set; }
    }
}