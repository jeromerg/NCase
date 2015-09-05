using Castle.Core.Internal;
using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.InterfaceRecPlay
{
    public class ReplayVisitors
        : IVisitor<INode, IReplayDirector, IInterfaceRecPlayNode>
    {
        public void Visit(IReplayDirector dir, IInterfaceRecPlayNode node)
        {
            node.IsReplay = dir.IsReplay;
            node.Children.ForEach(c => dir.Visit(c));
        }
    }
}
