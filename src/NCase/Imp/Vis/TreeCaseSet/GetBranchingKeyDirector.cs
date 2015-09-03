using System.Collections.Generic;
using NCase.Api.Vis;
using NDsl.Api.Core.Nod;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class GetBranchingKeyDirector 
        : Director<INode, IGetBranchingKeyDirector>
        , IGetBranchingKeyDirector
    {
        public object BranchingKey { get; set; }

        public GetBranchingKeyDirector(IEnumerable<IVisitorClass<INode, IGetBranchingKeyDirector>> visitors)
            : base(visitors)
        {
        }
    }
}