using System;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class CaseTreeSetNode : CaseTreeNodeAbstract, ICaseTreeSetNode
    {
        [NotNull] private readonly TreeCaseSet mCaseSet;

        public CaseTreeSetNode(
            [NotNull] TreeCaseSet caseSet, 
            [NotNull] ICodeLocation codeLocation,
            [NotNull] IGetBranchingKeyDirector getBranchingKeyDirector)
            : base(codeLocation, getBranchingKeyDirector)
        {
            if (caseSet == null) throw new ArgumentNullException("caseSet");
            mCaseSet = caseSet;
        }

        public ICaseSet CaseSet
        {
            get { return mCaseSet; }
        }

        public override string ToString()
        {
            return string.Format("CaseSet: {0}", mCaseSet);
        }
    }
}