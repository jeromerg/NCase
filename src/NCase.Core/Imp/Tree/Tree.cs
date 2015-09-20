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

namespace NCase.Imp.Tree
{
    public class Tree : ITree
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        private bool mIsDefined;

        #region Ctor and Factory
        
        public Tree(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter, 
            [NotNull] string defName,
            [NotNull] ICodeLocationUtil codeLocationUtil) 
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (defName == null) throw new ArgumentNullException("defName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mParserGenerator = parserGenerator;
            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
            mCodeLocationUtil = codeLocationUtil;
        }

        #endregion

        /// <exception cref="InvalidSyntaxException">Case set has already been defined</exception>
        public IDisposable Define()
        {
            if (mIsDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Definition {0} has already been defined", mDefName);
            }

            mIsDefined = true;

            return new DisposableWithCallbacks(OnBegin, OnEnd);
        }

        private void OnBegin()
        {
            mTokenReaderWriter.Append(new BeginToken<ITree>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        private void OnEnd()
        {
            mTokenReaderWriter.Append(new EndToken<ITree>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<ITree>(this, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public IEnumerable<ICase> Cases
        {
            get { return mParserGenerator.ParseAndGenerate(this, mTokenReaderWriter); }
        }
    }
}