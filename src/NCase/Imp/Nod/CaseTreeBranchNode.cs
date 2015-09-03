using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core.Ex;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NDsl.Api.RecPlay;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseTreeBranchNode : CaseTreeNodeAbstract, ICaseTreeBranchNode
    {
        [NotNull] private readonly INode mFact;

        public CaseTreeBranchNode(
            [NotNull] INode fact, 
            [NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
            : base(fact.CodeLocation, getBranchingKeyDirector)
        {
            if (fact == null) throw new ArgumentNullException("fact");
            mFact = fact;
        }

        public override IEnumerable<INode> Children
        {
            get
            {
                yield return mFact;
                foreach (var caseBranchNode in Branches)
                    yield return caseBranchNode;
            }
        }

        public INode Fact
        {
            get { return mFact; }
        }

        public override string ToString()
        {
            return string.Format("Fact: {0}, Branches: {1}", Fact, Branches.Count());
        }
    }
}