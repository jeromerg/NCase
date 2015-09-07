using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.InterfaceRecPlay
{
    public class GetBranchingKeyVisitors
        : IVisitor<INode, IGetBranchingKeyDirector, IInterfaceRecPlayNode>
    {
        public void Visit(IGetBranchingKeyDirector dir, IInterfaceRecPlayNode node)
        {
            dir.BranchingKey = node.PropertyCallKey;
        }
    }
}
