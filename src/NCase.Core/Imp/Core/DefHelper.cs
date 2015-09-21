using System;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Ex;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NDsl.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class DefHelper<TDef> : IDefHelper
        where TDef : IDef
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ISetFactory mSetFactory;
        [NotNull] private readonly TDef mDef;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        public DefHelper([NotNull] TDef def,
                         [NotNull] string defName,
                         [NotNull] ITokenReaderWriter tokenReaderWriter,
                         [NotNull] ICodeLocationUtil codeLocationUtil,
                         [NotNull] IParserGenerator parserGenerator,
                         [NotNull] ISetFactory setFactory)
        {
            if (def == null) throw new ArgumentNullException("def");
            if (defName == null) throw new ArgumentNullException("defName");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (setFactory == null) throw new ArgumentNullException("setFactory");

            mDef = def;
            mParserGenerator = parserGenerator;
            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
            mCodeLocationUtil = codeLocationUtil;
            mSetFactory = setFactory;
        }

        public DefSteps State { get; private set; }

        public ISet Cases
        {
            get { return mSetFactory.Create(mParserGenerator.ParseAndGenerate(mDef, mTokenReaderWriter)); }
        }

        public TResult Get<TResult>(ITransform<IDef, TResult> transform)
        {
            throw new NotImplementedException();
        }

        public IDisposable Define()
        {
            return new DisposableWithCallbacks(Begin, End);
        }

        public void Begin()
        {
            if (State > DefSteps.NotDefined)
            {
                throw new InvalidSyntaxException(mCodeLocationUtil.GetCurrentUserCodeLocation(),
                                                 "Case set {0} has already been defined",
                                                 mDefName);
            }

            State = DefSteps.Defining;
            mTokenReaderWriter.Append(new BeginToken<TDef>(mDef, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void End()
        {
            mTokenReaderWriter.Append(new EndToken<TDef>(mDef, mCodeLocationUtil.GetCurrentUserCodeLocation()));
            State = DefSteps.Defined;
        }

        public void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<TDef>(mDef, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }
    }
}