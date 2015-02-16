using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NCase.Impl.Vis
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, ICaseSetNode>
    {
        public void Visit(IDumpDirector director, ICaseSetNode node)
        {
            director.AddText("CASE_ROOT");
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
