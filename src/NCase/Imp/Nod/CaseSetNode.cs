using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Nod;
using NDsl.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseSetNode : ICaseSetNode
    {
        private readonly TreeCaseSet mCaseSetName;
        private readonly List<INode> mChildren = new List<INode>();

        public CaseSetNode([NotNull] TreeCaseSet caseSetName)
        {
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");
            mCaseSetName = caseSetName;
        }

        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }

        public ICaseSet CaseSet
        {
            get { return mCaseSetName; }
        }
    }
}