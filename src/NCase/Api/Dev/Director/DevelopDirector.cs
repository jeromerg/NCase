using System.Collections.Generic;
using System.Text;
using NVisitor.Api;
using NVisitor.Api.Marker;

namespace NCase.Api.Dev.Director
{
    public class DevelopDirector : Director<INode, DevelopDirector>
    {

        public DevelopDirector(IEnumerable<IVisitor<INode, DevelopDirector>> visitors)
            : base(visitors)
        {
        }

        public IEnumerable<List<INode>> FlattenCases { get; set; }
    }
}