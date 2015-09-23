using System.Linq;
using NCase.Back.Api.Parse;
using NCase.Back.Api.Tree;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.RecPlay;

namespace NCase.Back.Imp.Tree
{
    public class AddChildVisitors
        : IAddChildVisitor<ITreeNode, INode>,
          IAddChildVisitor<ITreeNode, IInterfaceRecPlayNode>
    {
        public void Visit(IAddChildDirector dir, ITreeNode parent, IInterfaceRecPlayNode child)
        {
            ICodeLocation codeLocation = child.CodeLocation;
            var nodeToAdd = new TreeNode(codeLocation, null, child);

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

            var nextInterfaceRecPlayNode = nextTreeNode.Fact as IInterfaceRecPlayNode;
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

        public void Visit(IAddChildDirector dir, ITreeNode parent, INode child)
        {
            INode nextBranch = parent.Branches.LastOrDefault();

            if (nextBranch == null)
                parent.AddTreeBranch(child);
            else
                dir.Visit(nextBranch, child);
        }
    }
}