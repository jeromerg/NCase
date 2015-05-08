using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Api.Nod;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseBranchNode : ICaseBranchNode
    {
        [NotNull] private readonly List<INode> mChildren;

        public CaseBranchNode([NotNull] INode caseFactAtThisLevel)
        {
            if (caseFactAtThisLevel == null) throw new ArgumentNullException("caseFactAtThisLevel");

            mChildren = new List<INode> {caseFactAtThisLevel};
        }

        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }

        public INode CaseFact
        {
            get { return mChildren[0]; }
        }

        public IEnumerable<INode> SubBranches
        {
            get { return mChildren.Skip(1); }
        }

        public ICodeLocation CodeLocation
        {
            get { return CaseFact.CodeLocation; }
        }

        public override string ToString()
        {
            return string.Format("CaseFact: {0}, SubBranches: {1}", CaseFact, SubBranches.Count());
        }
    }
}