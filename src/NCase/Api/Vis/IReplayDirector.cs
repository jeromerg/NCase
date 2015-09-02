using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Vis
{
    public interface IReplayDirector : IDirector<INode, IReplayDirector>
    {
        bool IsReplay { get; set; }
    }
}