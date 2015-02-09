using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NDsl.Impl.RecPlay
{
    public class PlayVisitors 
        : IVisitor<INode, IRePlayDir, INode>
        , IVisitor<INode, IRePlayDir, IRecPlayInterfacePropertyNode>
    {
        public void Visit(IRePlayDir director, INode node)
        {
            // default behavior: do nothing
        }

        public void Visit(IRePlayDir director, IRecPlayInterfacePropertyNode node)
        {
            node.Replay();
        }
    }
}