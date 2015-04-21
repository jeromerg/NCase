using Castle.Core.Internal;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis
{
    public class ReplayVisitors
        : IVisitor<INode, IReplayDirector, INode>
        , IVisitor<INode, IReplayDirector, IRecPlayInterfacePropertyNode>
    {
        public void Visit(IReplayDirector dir, INode node)
        {
            node.Children.ForEach(c => dir.Visit(c));
        }

        public void Visit(IReplayDirector dir, IRecPlayInterfacePropertyNode node)
        {
            node.IsReplay = dir.IsReplay;
            node.Children.ForEach(c => dir.Visit(c));
        }
    }
}
