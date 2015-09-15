using Castle.Core.Internal;
using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.Core.Nod;

namespace NCase.Imp.Core.Replay
{
    public class ReplayVisitors : IReplayVisitor<INode>
    {
        public void Visit(IReplayDirector dir, INode node)
        {
            node.Children.ForEach(c => dir.Visit(c));
        }
    }
}
