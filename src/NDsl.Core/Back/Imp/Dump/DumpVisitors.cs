using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Dump;
using NVisitor.Api.Action;

namespace NDsl.Back.Imp.Dump
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, INode>
    {
        public void Visit(IDumpDirector director, INode node)
        {
            director.AddText("{0}: {1}", node.GetType().Name, node);
            VisitNextLevel(director, node.Children);
        }

        private static void VisitNextLevel(IDumpDirector director, IEnumerable<INode> children)
        {
            director.Indent();
            children.ForEach(c => director.Visit(c));
            director.Dedent();
        }
    }
}