using Castle.Core.Internal;
using NCase.Back.Api.Replay;
using NDsl.Api.Core;

namespace NCase.Back.Imp.Replay
{
    public class ReplayVisitors : IReplayVisitor<INode>
    {
        public void Visit(IReplayDirector dir, INode node, bool isReplay)
        {
            node.Children.ForEach(c => dir.Visit(c, isReplay));
        }
    }
}