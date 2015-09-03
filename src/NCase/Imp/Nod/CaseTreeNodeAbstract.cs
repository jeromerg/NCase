using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public abstract class CaseTreeNodeAbstract : ICaseTreeNodeAbstract
    {
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly List<ICaseTreeBranchNode> mBranches = new List<ICaseTreeBranchNode>();
        [NotNull] private readonly IGetBranchingKeyDirector mGetBranchingKeyDirector;

        protected CaseTreeNodeAbstract(
            [NotNull] ICodeLocation codeLocation, 
            [NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (getBranchingKeyDirector == null) throw new ArgumentNullException("getBranchingKeyDirector");

            mCodeLocation = codeLocation;
            mGetBranchingKeyDirector = getBranchingKeyDirector;
        }

        public virtual IEnumerable<INode> Children
        {
            get { return Branches; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }


        public IEnumerable<ICaseTreeNodeAbstract> Branches
        {
            get { return mBranches; }
        }

        public void PlaceNextNode(INode child)
        {
            ICaseTreeBranchNode branch = mBranches.LastOrDefault();
            if (branch == null)
            {
                mBranches.Add(new CaseTreeBranchNode(child, mGetBranchingKeyDirector));
                return;
            }

            mGetBranchingKeyDirector.BranchingKey = null;
            mGetBranchingKeyDirector.Visit(branch);
            object branchBranchingKey = mGetBranchingKeyDirector.BranchingKey;

            if (branchBranchingKey == null)
            {
                throw new InvalidSyntaxException("Cannot place the node\n\t{0} under the node\n\t{1}",
                    child.CodeLocation, branch.CodeLocation);
            }

            mGetBranchingKeyDirector.BranchingKey = null;
            mGetBranchingKeyDirector.Visit(child);
            object childBranchingKey = mGetBranchingKeyDirector.BranchingKey;

            if (Equals(childBranchingKey, branchBranchingKey))
            {
                mBranches.Add(new CaseTreeBranchNode(child, mGetBranchingKeyDirector));
                return;
            }

            // Recursion
            branch.PlaceNextNode(child);
        }
    }
}