using System.Linq;
using NCase.Api.Dev;
using NCase.Api.Dev.Dir;
using NCase.Base;
using NCase.Component.InterfaceProxy;
using NVisitor.Api;

namespace NCase.Visit.Dump
{
    public class DumpVisitors
        : IVisitor<INode<ITarget>, DumpDirector, RootNode>
        , IVisitor<INode<ITarget>, DumpDirector, InterfaceProxyPropertyNode>
    {
        public void Visit(DumpDirector director, RootNode node)
        {
            director.AddText("ROOT");

            VisitChildren(director, node);
        }

        public void Visit(DumpDirector director, InterfaceProxyPropertyNode node)
        {
            if (node.InvocationAndFrame == null)
                return;

            string methodName = node.InvocationAndFrame.Invocation.Method.Name;
            string arguments = node.InvocationAndFrame.Invocation.Arguments
                                .Aggregate("", (s, o) => s + ", " + o)
                                .Trim(',', ' ');

            director.AddText("{0}({1})", methodName, arguments);

            VisitChildren(director, node);
        }

        private static void VisitChildren(DumpDirector director, INode<ITarget> node)
        {
            director.Indent();
            node.Children.ForEach(c => director.Visit(c));
            director.Dedent();
        }
    }
}
