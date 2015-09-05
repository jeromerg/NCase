using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Dev;
using NCase.Imp.Tree;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProductNode : IProductNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mBranches = new List<INode>();
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        [CanBeNull] private readonly ProductCaseSet mCaseSet;
        [CanBeNull] private readonly INode mFact;

        public ProductNode(
            [NotNull] ICodeLocation codeLocation, 
            [NotNull] IGetBranchingKeyDirector getBranchingKeyDirector,
            [CanBeNull] ProductCaseSet caseSet,
            [CanBeNull] INode fact)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");

            mCodeLocation = codeLocation;
            mGetBranchingKeyDirector = getBranchingKeyDirector;
            
            mCaseSet = caseSet;
            mFact = fact;

        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFact;
                foreach (var caseBranchNode in Branches)
                    yield return caseBranchNode;
            }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public ICaseSet CaseSet
        {
            get { return mCaseSet; }
        }

        public INode Fact
        {
            get { return mFact; }
        }

        public IEnumerable<INode> Branches
        {
            get { return mBranches; }
        }

        public void PlaceNextNode(INode child)
        {
            // prepare node to add
            object childBranchingKey = GetBranchingKey(child);
            INode nodeToAdd = (childBranchingKey == null)
                ? child
                : new CaseTreeNode(child.CodeLocation, mGetBranchingKeyDirector, null, child);

            INode lastBranch = mBranches.LastOrDefault();

            // End of recursion: end of branch: place the child here
            if (lastBranch == null)
            {
                mBranches.Add(nodeToAdd);
                return;
            }

            ICaseTreeNode lastBranchAsTreeNode = lastBranch as ICaseTreeNode;
            if (lastBranchAsTreeNode == null)
                throw new InvalidSyntaxException("Last branch is an end branch and therefore cannot accept any child: {0}", lastBranch.CodeLocation);

            // No Fact, no risk, then recurse
            if (lastBranchAsTreeNode.Fact == null)
            {
                lastBranchAsTreeNode.PlaceNextNode(child);
                return;
            }

            object branchBranchingKey = GetBranchingKey(lastBranchAsTreeNode.Fact);
            if (branchBranchingKey == null)
            {
                throw new InvalidSyntaxException("Cannot place the node\n\t{0} under the following node, that doesn't accept children\n\t{1}",
                    child.CodeLocation, lastBranchAsTreeNode.CodeLocation);
            }

            if (Equals(childBranchingKey, branchBranchingKey))
            {
                // End of recursion: place the child here, as it has the same branching key as the last branch (sibling)
                mBranches.Add(nodeToAdd);
                return;
            }

            // Recursion
            lastBranchAsTreeNode.PlaceNextNode(child);
        }

        private object GetBranchingKey(INode child)
        {
            object childBranchingKey;
            {
                mGetBranchingKeyDirector.BranchingKey = null;
                mGetBranchingKeyDirector.Visit(child);
                childBranchingKey = mGetBranchingKeyDirector.BranchingKey;
            }
            return childBranchingKey;
        }
    }
}