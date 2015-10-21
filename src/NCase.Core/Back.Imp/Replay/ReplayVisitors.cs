using Castle.Core.Internal;
using NCaseFramework.Back.Api.Replay;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Replay
{
    public class ReplayVisitors : IReplayVisitor<INode>
    {
        public void Visit(IReplayDirector dir, INode node, bool isReplay)
        {
            node.Children.ForEach(c => dir.Visit(c, isReplay));
        }
    }
}