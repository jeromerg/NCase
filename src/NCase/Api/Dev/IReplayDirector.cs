using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev
{
    public interface IReplayDirector : IDirector<INode, IReplayDirector>
    {
        bool IsReplay { get; set; }
    }
}