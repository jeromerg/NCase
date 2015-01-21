using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using NCase.Api.Dev;
using NCase.Api.Dev.Director;
using NCase.Core.InterfaceContrib;
using NVisitor.Api;

namespace NCase.Core.Visitor
{
    public class DumpVisitors
        : IVisitor<INode, DumpDirector, AstNode>
        , IVisitor<INode, DumpDirector, RootNode>
        , IVisitor<INode, DumpDirector, InterfacePropertyNode>
    {
        public void Visit(DumpDirector director, AstNode node)
        {
            director.AddText("AST");
            VisitNextLevel(director, node.Roots);
        }

        public void Visit(DumpDirector director, RootNode node)
        {
            director.AddText("ROOT");
            VisitNextLevel(director, node.Children);
        }

        public void Visit(DumpDirector director, InterfacePropertyNode node)
        {
            string methodName = node.Invocation.Method.Name;
            string arguments = node.Invocation.Arguments
                                .Aggregate("", (s, o) => s + ", " + o)
                                .Trim(',', ' ');

            director.AddText("{0}({1}), in {2}: line {3}", 
                             methodName, 
                             arguments, 
                             node.StackFrame.GetFileName(),
                             node.StackFrame.GetFileLineNumber());
        }

        private static void VisitNextLevel(DumpDirector director, IEnumerable<INode> children)
        {
            director.Indent();
            children.ForEach(c => director.Visit(c));
            director.Dedent();
        }
    }
}
