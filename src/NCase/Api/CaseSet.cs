using System;
using NDsl.Api.Core;
using NDsl.Imp.Core.Semantic;
using NDsl.Imp.Core.Token;
using NVisitor.Common.Quality;

namespace NCase.Api
{
    public class CaseSet
    {
        [NotNull] private readonly IAstRoot mAstRoot;
        [NotNull] private readonly string mCaseSetName;

        private bool mIsDefined;

        public CaseSet([NotNull] IAstRoot astRoot, [NotNull] string caseSetName) 
        {
            if (astRoot == null) throw new ArgumentNullException("astRoot");
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");

            mAstRoot = astRoot;
            mCaseSetName = caseSetName;
        }

        public IDisposable Define()
        {
            if (mIsDefined)
                throw new InvaliSyntaxException("Case set {0} has already been defined", mCaseSetName);

            mIsDefined = true;
            return new SemanticalBlockDisposable<CaseSet>(mAstRoot, this);
        }

        public void Ref()
        {
            mAstRoot.AddChild(new RefToken<CaseSet>(this));
        }

    }
}