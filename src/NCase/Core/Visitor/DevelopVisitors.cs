using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev;
using NCase.Api.Dev.Director;
using NVisitor.Api;

namespace NCase.Core.Visitor
{
    public class DevelopVisitors
        : IVisitor<INode, DevelopDirector, RootNode>
        , IVisitor<INode, DevelopDirector, INode>
    {

        public void Visit(DevelopDirector director, RootNode node)
        {
            if (director.FlattenCases != null)
                throw new ArgumentException("DevelopDirector can only visit a single RootNode and the current director has already visited one");

            director.FlattenCases = BuildFlattenCasesEnumerable(director, node);
        }

        private IEnumerable<List<INode>> BuildFlattenCasesEnumerable(DevelopDirector director, RootNode node)
        {
            if(node.Children.Count == 0)
                yield break;

            foreach (var child in node.Children)
            {
                
            }
        }

        public void Visit(DevelopDirector director, INode node)
        {
            //VisitChildren(director, node);
        }

        //private static void VisitChildren(DevelopDirector director, INode node)
        //{
        //    node.Children.ForEach(c => director.Visit(c));
        //}
    }
}
