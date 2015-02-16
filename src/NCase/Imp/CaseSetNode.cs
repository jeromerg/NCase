using System;
using System.Collections.Generic;
using NCase.Api;
using NDsl.Api.Core;
using NVisitor.Common.Quality;

namespace NCase.Impl
{
    public class CaseSetNode : ICaseSetNode
    {
        private readonly string mCaseSetName;
        private readonly List<INode> mChildren = new List<INode>();

        public CaseSetNode([NotNull] string caseSetName)
        {
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");
            mCaseSetName = caseSetName;
        }

        public IEnumerable<INode> Children { get { return mChildren; } } 
    }
}