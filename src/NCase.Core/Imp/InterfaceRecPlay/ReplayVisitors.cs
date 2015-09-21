using Castle.Core.Internal;
using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.RecPlay;

namespace NCase.Imp.InterfaceRecPlay
{
    public class ReplayVisitors
        : IReplayVisitor<IInterfaceRecPlayNode>
    {
        public void Visit(IReplayDirector dir, IInterfaceRecPlayNode node, bool isReplay)
        {
            node.IsReplay = isReplay;
            node.Children.ForEach(c => dir.Visit(c, isReplay));
        }
    }
}