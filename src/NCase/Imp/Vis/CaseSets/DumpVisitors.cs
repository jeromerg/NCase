using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Nod;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Vis;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.CaseSets
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, ICaseTreeSetNode>
        , IVisitor<INode, IDumpDirector, ICaseTreeBranchNode>
    {
        public void Visit(IDumpDirector dir, ICaseTreeSetNode node)
        {
            dir.AddText("CASE_TREE_ROOT");
            VisitNextLevel(dir, node.Children);
        }

        public void Visit(IDumpDirector dir, ICaseTreeBranchNode node)
        {
            dir.AddText("CASE_TREE_NODE: fact=" + node.Fact);
            VisitNextLevel(dir, node.Branches);
        }

        private static void VisitNextLevel(IDumpDirector dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }
    }
}
