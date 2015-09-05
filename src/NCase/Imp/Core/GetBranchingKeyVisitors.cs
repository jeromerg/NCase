using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Core
{
    public class GetBranchingKeyVisitors
        : IVisitor<INode, IGetBranchingKeyDirector, INode>
    {
        public void Visit(IGetBranchingKeyDirector dir, INode node)
        {
            // do nothing: Director.BranchingKey remains null, meaning 
            // the node doesn't participate to any branching: it is and end node
            dir.BranchingKey = null;
        }
    }
}
