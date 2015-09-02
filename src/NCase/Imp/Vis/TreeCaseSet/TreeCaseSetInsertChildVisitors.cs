using System;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class TreeCaseSetInsertChildVisitors
        : IVisitor<INode, ITreeCaseSetInsertChildDirector, INode>
        , IVisitor<INode, ITreeCaseSetInsertChildDirector, IInterfaceRecPlayNode>
        , IVisitor<INode, ITreeCaseSetInsertChildDirector, IRefNode<ICaseSetNode>>
    {
        public void Visit(ITreeCaseSetInsertChildDirector dir, INode node)
        {
            throw new InvalidSyntaxException("Cannot insert unsupported type {0} into TreeCaseSet, Location:{1}", 
                node.GetType().FullName, node.CodeLocation);
        }

        public void Visit(ITreeCaseSetInsertChildDirector director, IInterfaceRecPlayNode node)
        {
            INode parent = FindPlaceInTreeToAddPropertyCall(director.CurrentParentCandidate, director.CurrentParentCandidate.Children.LastOrDefault(), node);

            var parentToAddTo = parent as IExtendableNode;
            if (parentToAddTo == null)
            {
                throw new InvalidCaseRecordException("Can not attach case record to parent.\nCase record: {0}\nParent: {1}",
                    node.CodeLocation, parent.CodeLocation);
            }

            // wrap node into CaseBranchNode and add it to parent
            var caseBranchNode = new CaseBranchNode(node);
            parentToAddTo.AddChild(caseBranchNode);

        }

        public void Visit(ITreeCaseSetInsertChildDirector director, IRefNode<ICaseSetNode> nodeToAdd)
        {
            IExtendableNode lastLeaf = FindLastLeafRecursive(director.CurrentParentCandidate);
            lastLeaf.AddChild(nodeToAdd);
        }

        private INode FindPlaceInTreeToAddPropertyCall(INode parentCandidate, INode siblingCandidate, IInterfaceRecPlayNode node)
        {
            if (siblingCandidate == null)
                return parentCandidate;

            var branch = siblingCandidate as ICaseBranchNode;
            if(branch == null)
                throw new ArgumentException("Recursion should only recurse along the ICaseBranchNode children");

            var caseFact = branch.Fact as IInterfaceRecPlayNode;
            if (caseFact == null)
                return FindPlaceInTreeToAddPropertyCall(branch, branch.SubBranches.LastOrDefault(), node);

            // FORK, if candidate has same PropertyCallKey as node
            if (Equals(caseFact.PropertyCallKey, node.PropertyCallKey))
                return parentCandidate;

            return FindPlaceInTreeToAddPropertyCall(branch, branch.SubBranches.LastOrDefault(), node);
        }

        private IExtendableNode FindLastLeafRecursive(INode node)
        {
            var caseBranchNode = node as ICaseBranchNode;
            if (caseBranchNode != null)
            {
                INode child = caseBranchNode.SubBranches.LastOrDefault();
                if (child == null)
                    return caseBranchNode;

                return FindLastLeafRecursive(child);
            }

            if(node is IExtendableNode)
                return (IExtendableNode) node;
            
            throw new InvalidSyntaxException("Current node of type {0} is not extendable", node.GetType().FullName);
        }
    }
}
