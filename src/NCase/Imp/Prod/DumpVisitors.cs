﻿using System.Collections.Generic;
using Castle.Core.Internal;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Vis;
using NVisitor.Api.Batch;

namespace NCase.Imp.Prod
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, IProductNode>
    {
        public void Visit(IDumpDirector dir, IProductNode node)
        {
            dir.AddText("CardinalProduct: {0}", node);
            VisitNextLevel(dir, node.Children);
        }

        private static void VisitNextLevel(IDumpDirector dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }
    }
}
