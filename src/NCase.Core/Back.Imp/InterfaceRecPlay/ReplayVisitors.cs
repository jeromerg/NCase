using Castle.Core.Internal;
using NCase.Back.Api.Replay;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.InterfaceRecPlay
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