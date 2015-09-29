using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Back.Api.Pairwise;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Dump;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Pairwise
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, IPairwiseNode>
    {
        public void Visit(IDumpDirector dir, IPairwiseNode node)
        {
            dir.AddText("Pairwise: {0}", node);
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