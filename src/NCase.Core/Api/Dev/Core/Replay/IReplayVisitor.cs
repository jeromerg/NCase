using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Core.Replay
{
    public interface IReplayVisitor<TNod> : IVisitor<INode, IReplayDirector, TNod> 
        where TNod : INode
    {
    }
}