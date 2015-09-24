using System.Collections.Generic;
using Castle.Core.Internal;
using NCase.Back.Api.Tree;
using NDsl.Api.Core;
using NDsl.Api.Dump;
using NVisitor.Api.Action;

namespace NCase.Back.Imp.Tree
{
    public class DumpVisitors
        : IActionVisitor<INode, IDumpDirector, ITreeNode>
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