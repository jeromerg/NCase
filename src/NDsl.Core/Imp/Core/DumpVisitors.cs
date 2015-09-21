using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NVisitor.Api.Action;

namespace NDsl.Imp.Core
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, INode>
    {
        private static void VisitNextLevel(IDumpDirector director, IEnumerable<INode> children)
        {
            director.Indent();
            children.ForEach(c => director.Visit(c));
            director.Dedent();
        }

        public void Visit(IDumpDirector director, INode node)
        {
            director.AddText("{0}: {1}", node.GetType().Name, node);
            VisitNextLevel(director, node.Children);
        }
    }
}