using Castle.Core.Internal;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Replay;
using NDsl.Back.Api.Common;

namespace NCaseFramework.Back.Imp.Replay
{
    public class ReplayVisitors : IReplayVisitor<INode>
    {
        public void Visit([NotNull] IReplayDirector dir, [NotNull] INode node, bool isReplay)
        {
            node.Children.ForEach(c => dir.Visit(c, isReplay));
        }
    }
}