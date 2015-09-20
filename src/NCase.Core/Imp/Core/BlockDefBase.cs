using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NDsl.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public abstract class BlockDefBase<TDef> : IBlockDefBase
        where TDef : IDef
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        public BlockDefBase(
            [NotNull] IParserGenerator parserGenerator,
            [NotNull] ITokenReaderWriter tokenReaderWriter,
            [NotNull] string defName,
            [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (defName == null) throw new ArgumentNullException("defName");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");

            mParserGenerator = parserGenerator;
            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
            mCodeLocationUtil = codeLocationUtil;
        }

        protected DefSteps State { get; private set; }
        protected abstract TDef GetDef();

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public virtual void Begin()
        {
            if (State > DefSteps.NotDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined", mDefName);
            }

            State = DefSteps.Defining;
            mTokenReaderWriter.Append(new BeginToken<TDef>(GetDef(), mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public virtual void End()
        {
            mTokenReaderWriter.Append(new EndToken<TDef>(GetDef(), mCodeLocationUtil.GetCurrentUserCodeLocation()));
            State = DefSteps.Defined;
        }

        public virtual void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<TDef>(GetDef(), mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public virtual IEnumerable<ICase> Cases
        {
            get { return mParserGenerator.ParseAndGenerate(GetDef(), mTokenReaderWriter); }
        }
    }
}
