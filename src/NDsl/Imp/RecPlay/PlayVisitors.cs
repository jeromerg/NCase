using NDsl.Api.Core;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NDsl.Imp.RecPlay
{
    public class PlayVisitors 
        : IVisitor<INode, IRePlayDirector, INode>
        , IVisitor<INode, IRePlayDirector, IRecPlayInterfacePropertyNode>
    {
        public void Visit(IRePlayDirector director, INode node)
        {
            // default behavior: do nothing
        }

        public void Visit(IRePlayDirector director, IRecPlayInterfacePropertyNode node)
        {
            node.Replay();
        }
    }
}