using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Dev.Prod;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NVisitor.Api.Action;

namespace NCase.Imp.Prod
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, IProdNode>
    {
        public void Visit(IDumpDirector dir, IProdNode node)
        {
            dir.AddText("CartesianProduct: {0}", node);
            VisitNextLevel(dir, node.Children);
        }

        private static void VisitNextLevel(IDumpDirector dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }
    }
}
