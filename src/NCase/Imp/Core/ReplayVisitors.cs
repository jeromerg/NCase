using Castle.Core.Internal;
using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core
{
    public class ReplayVisitors
        : IVisitor<INode, IReplayDirector, INode>
    {
        public void Visit(IReplayDirector dir, INode node)
        {
            node.Children.ForEach(c => dir.Visit(c));
        }
    }
}
