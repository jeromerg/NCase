using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Tree;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.RecPlay;

namespace NCaseFramework.Back.Imp.Tree
{
    public class AddChildVisitors
        : IAddChildVisitor<ITreeNode, INode>,
          IAddChildVisitor<ITreeNode, IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ITreeNode parent, [NotNull] IInterfaceRecPlayNode child)
        {
            var nodeToAdd = new TreeNode(child.CodeLocation, new TreeId(), child);

            ITreeNode nextBranch = parent.Branches.LastOrDefault();

            // no branch => end of recursion: this is the place
            if (nextBranch == null)
            {
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            var nextInterfaceRecPlayNode = nextBranch.CasesOfThisTreeNode as IInterfaceRecPlayNode;
            if (nextInterfaceRecPlayNode != null
                && Equals(nextInterfaceRecPlayNode.PropertyCallKey, child.PropertyCallKey))
            {
                // same key, then fork here
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            // ok keys are different, recurse
            dir.Visit(nextBranch, child);
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ITreeNode parent, [NotNull] INode child)
        {
            var nodeToAdd = new TreeNode(child.CodeLocation, new TreeId(), child);

            INode nextBranch = parent.Branches.LastOrDefault();

            if (nextBranch == null)
                parent.AddTreeBranch(nodeToAdd);
            else
                dir.Visit(nextBranch, child);
        }
    }
}