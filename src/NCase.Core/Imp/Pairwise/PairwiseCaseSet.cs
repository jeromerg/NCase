using System;
using NCase.Api;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class PairwiseCaseSet : IPairwise
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ITokenWriter mTokenWriter;
        [NotNull] private readonly string mCaseSetName;

        private bool mIsDefined;

        #region Ctor and Factory

        /// <exception cref="ArgumentNullException">The value of 'tokenWriter'/'caseSetName' cannot be null. </exception>
        public PairwiseCaseSet(
            [NotNull] ITokenWriter tokenWriter,
            [NotNull] string caseSetName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (tokenWriter == null) throw new ArgumentNullException("tokenWriter");
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mTokenWriter = tokenWriter;
            mCaseSetName = caseSetName;
            mCodeLocationUtil = codeLocationUtil;
        }

        #endregion

        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        public IDisposable Define()
        {
            if (mIsDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined", mCaseSetName);
            }

            mIsDefined = true;

            return new SemanticalBlockDisposable<PairwiseCaseSet>(mCodeLocationUtil, mTokenWriter, this);
        }

        public void Ref()
        {
            mTokenWriter.Append(new RefToken<PairwiseCaseSet>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

    }
}