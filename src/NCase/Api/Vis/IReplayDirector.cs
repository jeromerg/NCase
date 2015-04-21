using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IReplayDirector : IDirector<INode, IReplayDirector>
    {
        bool IsReplay { get; set; }
    }
}