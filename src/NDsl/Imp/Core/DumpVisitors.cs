using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Api.Core;
using NVisitor.Api.Batch;

namespace NDsl.Impl.Core
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, IAstRoot>
    {
        public void Visit(IDumpDirector director, IAstRoot node)
        {
            director.AddText("AST_ROOT");
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
