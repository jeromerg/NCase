using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Nod;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, ICaseSetNode>
    {
        public void Visit(IDumpDirector dir, ICaseSetNode node)
        {
            dir.AddText("CASE_ROOT");
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
