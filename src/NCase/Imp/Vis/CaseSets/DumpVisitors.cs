using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Imp.Nod;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Vis;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.CaseSets
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, ICaseTreeNode>
    {
        public void Visit(IDumpDirector dir, ICaseTreeNode node)
        {
            dir.AddText("TreeNode: {0}", node);
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
