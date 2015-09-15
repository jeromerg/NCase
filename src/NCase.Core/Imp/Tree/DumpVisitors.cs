﻿using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NVisitor.Api.Batch;

namespace NCase.Imp.Tree
{
    public class DumpVisitors
        : IVisitor<INode, IDumpDirector, ITreeNode>
    {
        public void Visit(IDumpDirector dir, ITreeNode node)
        {
            dir.AddText("TreeNode: {0}", node);
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
