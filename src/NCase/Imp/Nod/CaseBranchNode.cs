using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Nod;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseBranchNode : ICaseBranchNode
    {
        [NotNull] private readonly List<INode> mChildren;

        public CaseBranchNode([NotNull] INode fact)
        {
            if (fact == null) throw new ArgumentNullException("fact");

            mChildren = new List<INode> {fact};
        }

        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }

        public INode Fact
        {
            get { return mChildren[0]; }
        }

        public IEnumerable<INode> SubBranches
        {
            get { return mChildren.Skip(1); }
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