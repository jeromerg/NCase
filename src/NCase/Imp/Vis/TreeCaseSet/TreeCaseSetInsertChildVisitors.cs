using System;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NCase.Imp.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Ex;
using NDsl.Api.RecPlay;
using NVisitor.Api.Batch;

namespace NCase.Imp.Vis.TreeCaseSet
{
    public class TreeCaseSetInsertChildVisitors
        : IVisitor<INode, ITreeCaseSetInsertChildDirector, INode>
        , IVisitor<INode, ITreeCaseSetInsertChildDirector, IInterfaceRecPlayNode>
    {
        public void Visit(ITreeCaseSetInsertChildDirector dir, INode node)
        {
            throw new InvalidSyntaxException("Type of record not supported in a Tree Case Set, Location:{0}", node.CodeLocation);
        }

        public void Visit(ITreeCaseSetInsertChildDirector director, IInterfaceRecPlayNode node)
        {
            INode parent = FindPlaceInTreeToAddPropertyCall(director.Root, director.Root.Children.LastOrDefault(), node);

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

        private INode FindPlaceInTreeToAddPropertyCall(INode parentCandidate, INode siblingCandidate, IInterfaceRecPlayNode node)
        {
            if (siblingCandidate == null)
                return parentCandidate;

            var branch = siblingCandidate as ICaseBranchNode;
            if(branch == null)
                throw new ArgumentException("Recursion should only recurse along the ICaseBranchNode children");

            var caseFact = branch.CaseFact as IInterfaceRecPlayNode;
            if (caseFact == null)
                return FindPlaceInTreeToAddPropertyCall(branch, branch.SubBranches.LastOrDefault(), node);

            // FORK, if candidate has same PropertyCallKey as node
            if (Equals(caseFact.PropertyCallKey, node.PropertyCallKey))
                return parentCandidate;

            return FindPlaceInTreeToAddPropertyCall(branch, branch.SubBranches.LastOrDefault(), node);
        }
    }
}
