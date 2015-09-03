using NCase.Api.Vis;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class GetBranchingKeyVisitors
        : IVisitor<INode, IGetBranchingKeyDirector, INode>
        , IVisitor<INode, IGetBranchingKeyDirector, IInterfaceRecPlayNode>
    {
        public void Visit(IGetBranchingKeyDirector dir, INode node)
        {
            // do nothing: Director.BranchingKey remains null, 
            // and the CaseTreeSetNode.PlaceNextNode() algorithm
            // will throw an InvalidSyntaxException if it reaches this node
            dir.BranchingKey = null;
        }

        public void Visit(IGetBranchingKeyDirector dir, IInterfaceRecPlayNode node)
        {
            dir.BranchingKey = node.PropertyCallKey;
        }
    }
}
