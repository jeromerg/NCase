using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NDsl.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class Prod : IProd
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mCaseSetName;

        private bool mIsDefined;

        #region Ctor and Factory

        /// <exception cref="ArgumentNullException">The value of 'tokenWriter'/'caseSetName' cannot be null. </exception>
        public Prod(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter,
            [NotNull] string caseSetName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (caseSetName == null) throw new ArgumentNullException("caseSetName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mParserGenerator = parserGenerator;
            mTokenReaderWriter = tokenReaderWriter;
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


            return new DisposableWithCallbacks(OnBegin, OnEnd);
        }

        private void OnBegin()
        {
            mTokenReaderWriter.Append(new BeginToken<IProd>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        private void OnEnd()
        {
            mTokenReaderWriter.Append(new EndToken<IProd>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<IProd>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public IEnumerable<ICase> Cases
        {
            get { return mParserGenerator.ParseAndGenerate(this, mTokenReaderWriter); }
        }

    }
}