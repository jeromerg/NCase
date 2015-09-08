﻿using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Vis;
using NVisitor.Api.Batch;

namespace NDsl.Imp.Core
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, INode>
    {
        public void Visit(IDumpDirector director, INode node)
        {
            director.AddText("{0}: {1}", node.GetType().Name, node);
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