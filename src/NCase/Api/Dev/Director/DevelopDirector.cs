using System.Collections.Generic;
using NDsl.Api.Dev;
using NVisitor.Api.Batch;

namespace NCase.Api.Dev.Director
{
    public class DevelopDirector : Director<INode, DevelopDirector>
    {

        public DevelopDirector(IEnumerable<IVisitorClass<INode, DevelopDirector>> visitors)
            : base(visitors)
        {
        }

        public IEnumerable<List<INode>> FlattenCases { get; set; }
    }
}