using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NDsl.Impl.Core
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDir, IAstRoot>
    {
        public void Visit(IDumpDir dir, IAstRoot node)
        {
            dir.AddText("AST_ROOT");
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
