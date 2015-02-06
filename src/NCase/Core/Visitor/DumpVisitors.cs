using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using NCase.Api.Dev;
using NCase.Api.Dev.Director;
using NDsl.Api.Dev;
using NDsl.Core.InterfaceContrib;
using NVisitor.Api.Batch;

namespace NCase.Core.Visitor
{
    public class DumpVisitors
        : IVisitor<INode, DumpDirector, AstRoot>
        , IVisitor<INode, DumpDirector, CaseRootNode>
        , IVisitor<INode, DumpDirector, InterfacePropertyNode>
    {
        public void Visit(DumpDirector director, AstRoot node)
        {
            director.AddText("AST");
            VisitNextLevel(director, node.Children);
        }

        public void Visit(DumpDirector director, CaseRootNode node)
        {
            director.AddText("ROOT");
            VisitNextLevel(director, node.Children);
        }

        public void Visit(DumpDirector director, InterfacePropertyNode node)
        {
            director.AddText(node.CodeLocation.GetUserCodeInfo());
        }

        private static void VisitNextLevel(DumpDirector director, IEnumerable<INode> children)
        {
            director.Indent();
            children.ForEach(c => director.Visit(c));
            director.Dedent();
        }
    }
}
