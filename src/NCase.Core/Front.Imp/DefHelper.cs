using System;
using System.Collections.Generic;
using System.Linq;
using NCase.Back.Api.Core;
using NCase.Back.Api.Parse;
using NCase.Front.Api;
using NDsl.Back.Api.Block;
using NDsl.Back.Api.Core;
using NDsl.Back.Api.Ref;
using NVisitor.Common.Quality;

namespace NCase.Front.Imp
{
    public class DefHelper<TDefId> : IDefHelper
        where TDefId : IDefId
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly ISetFactory mSetFactory;
        private readonly ICaseFactory mCaseFactory;
        [NotNull] private readonly TDefId mDefId;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ITokenReaderWriter mTokenReaderWriter;
        [NotNull] private readonly string mDefName;

        public DefHelper([NotNull] TDefId defId,
                         [NotNull] string defName,
                         [NotNull] ITokenReaderWriter tokenReaderWriter,
                         [NotNull] ICodeLocationUtil codeLocationUtil,
                         [NotNull] IParserGenerator parserGenerator,
                         [NotNull] ISetFactory setFactory,
                         [NotNull] ICaseFactory caseFactory)
        {
            if (defId == null) throw new ArgumentNullException("defId");
            if (defName == null) throw new ArgumentNullException("defName");
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (setFactory == null) throw new ArgumentNullException("setFactory");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");

            mDefId = defId;
            mParserGenerator = parserGenerator;
            mTokenReaderWriter = tokenReaderWriter;
            mDefName = defName;
            mCodeLocationUtil = codeLocationUtil;
            mSetFactory = setFactory;
            mCaseFactory = caseFactory;
        }

        public DefSteps State { get; private set; }

        public ISet Cases
        {
            get
            {
                IEnumerable<ICase> cases =
                    mParserGenerator.ParseAndGenerate(mDefId, mTokenReaderWriter).Select(tc => mCaseFactory.Create(tc));
                return mSetFactory.Create(cases);
            }
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
            mTokenReaderWriter.Append(new BeginToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }

        public void End()
        {
            mTokenReaderWriter.Append(new EndToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
            State = DefSteps.Defined;
        }

        public void Ref()
        {
            mTokenReaderWriter.Append(new RefToken<TDefId>(mDefId, mCodeLocationUtil.GetCurrentUserCodeLocation()));
        }
    }
}