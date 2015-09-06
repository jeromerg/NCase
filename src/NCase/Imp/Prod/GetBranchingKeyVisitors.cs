using NCase.Api.Dev;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Prod
{
    public class GetBranchingKeyVisitors
        : IVisitor<INode, IGetBranchingKeyDirector, ProdDimNode>
    {
        public void Visit(IGetBranchingKeyDirector dir, ProdDimNode node)
        {
            dir.Visit(node.FirstChild);
        }
    }
}
