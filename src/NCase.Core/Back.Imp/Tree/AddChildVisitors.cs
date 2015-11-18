using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Parse;
using NCaseFramework.Back.Api.Tree;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.RecPlay;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Tree
{
    public class AddChildVisitors
        : IAddChildVisitor<ITreeNode, INode>,
          IAddChildVisitor<ITreeNode, IInterfaceRecPlayNode>
    {
        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ITreeNode parent, [NotNull] IInterfaceRecPlayNode child)
        {
            CodeLocation codeLocation = child.CodeLocation;
            var nodeToAdd = new TreeNode(codeLocation, new TreeId(), child);

            INode nextBranch = parent.Branches.LastOrDefault();

            if (nextBranch == null)
            {
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            var nextTreeNode = nextBranch as ITreeNode;
            if (nextTreeNode == null)
            {
                throw new InvalidSyntaxException(codeLocation,
                                                 "Last branch is an end branch and therefore cannot accept any child");
            }

            var nextInterfaceRecPlayNode = nextTreeNode.TreeFact as IInterfaceRecPlayNode;
            if (nextInterfaceRecPlayNode != null
                && Equals(nextInterfaceRecPlayNode.PropertyCallKey, child.PropertyCallKey))
            {
                // same key, then branch here
                parent.AddTreeBranch(nodeToAdd);
                return;
            }

            // ok keys are different, recurse
            dir.Visit(nextTreeNode, child);
        }

        public void Visit([NotNull] IAddChildDirector dir, [NotNull] ITreeNode parent, [NotNull] INode child)
        {
            INode nextBranch = parent.Branches.LastOrDefault();

            if (nextBranch == null)
                parent.AddTreeBranch(child);
            else
                dir.Visit(nextBranch, child);
        }
    }
}