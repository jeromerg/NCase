using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseBranchNode : ICaseBranchNode
    {
        [NotNull] private readonly INode mFact;
        [NotNull] private readonly List<ICaseBranchNode> mSubBranches;

        public CaseBranchNode([NotNull] INode fact)
        {
            if (fact == null) throw new ArgumentNullException("fact");

            mFact = fact;
            mSubBranches = new List<ICaseBranchNode>();
        }

        public IEnumerable<INode> Children
        {
            get
            {
                yield return mFact;
                foreach (var caseBranchNode in mSubBranches)
                    yield return caseBranchNode;
            }
        }

        public void PlaceNextChild(INode child)
        {
            mSubBranches.Add(child);
        }

        public INode Fact
        {
            get { return mSubBranches[0]; }
        }

        public IEnumerable<ICaseBranchNode> SubBranches
        {
            get { return mSubBranches.Skip(1); }
        }

        public ICodeLocation CodeLocation
        {
            get { return Fact.CodeLocation; }
        }

        public override string ToString()
        {
            return string.Format("Fact: {0}, SubBranches: {1}", Fact, SubBranches.Count());
        }
    }
}