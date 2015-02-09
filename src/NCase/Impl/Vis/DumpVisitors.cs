using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Impl.Vis
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDir, ICaseSetNode>
    {
        public void Visit(IDumpDir dir, ICaseSetNode node)
        {
            dir.AddText("CASE_ROOT");
            VisitNextLevel(dir, node.Children);
        }

        private static void VisitNextLevel(IDumpDir dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }
    }
}
