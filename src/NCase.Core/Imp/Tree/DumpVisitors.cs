﻿using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Vis;
using NVisitor.Api.Action;

namespace NCase.Imp.Tree
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, ITreeNode>
    {
        private static void VisitNextLevel(IDumpDirector dir, IEnumerable<INode> children)
        {
            dir.Indent();
            children.ForEach(c => dir.Visit(c));
            dir.Dedent();
        }

        public void Visit(IDumpDirector dir, ITreeNode node)
        {
            dir.AddText("TreeNode: {0}", node);
            VisitNextLevel(dir, node.Children);
        }
    }
}