using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Back.Api.Prod;
using NDsl.Api.Core;
using NDsl.Api.Dump;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Prod
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