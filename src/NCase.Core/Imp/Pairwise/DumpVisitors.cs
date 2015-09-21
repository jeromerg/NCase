using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Dev.Pairwise;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NVisitor.Api.Action;

namespace NCase.Imp.Pairwise
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, IPairwiseNode>
    {
        private static void VisitNextLevel(IDumpDirector dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }

        public void Visit(IDumpDirector dir, IPairwiseNode node)
        {
            dir.AddText("Pairwise: {0}", node);
            VisitNextLevel(dir, node.Children);
        }
    }
}