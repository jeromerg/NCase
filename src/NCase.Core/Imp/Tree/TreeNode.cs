using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Tree;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class TreeNode : ITreeNode
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<INode> mBranches = new List<INode>();

        [CanBeNull] private readonly TreeCaseSet mCaseSet;
        [CanBeNull] private readonly INode mFact;

        public TreeNode(
            [NotNull] ICodeLocation codeLocation, 
            [CanBeNull] TreeCaseSet caseSet,
            [CanBeNull] INode fact)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            mCodeLocation = codeLocation;
            
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

        public ITree CaseSet
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

        public void AddTreeBranch(INode branch)
        {
            mBranches.Add(branch);
        }
    }
}