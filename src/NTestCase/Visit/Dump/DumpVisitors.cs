using System.Linq;
using NTestCase.Api.Dev;
using NTestCase.Api.Dev.Dir;
using NTestCase.Base;
using NTestCase.Component.InterfaceProxy;
using NTestCase.Util.Visit;

namespace NTestCase.Visit.Dump
{
    public class DumpVisitors 
        : IVisitor<DumpDirector, INode<ITarget>, RootNode>
        , IVisitor<DumpDirector, INode<ITarget>, InterfaceProxyPropertyNode>
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
