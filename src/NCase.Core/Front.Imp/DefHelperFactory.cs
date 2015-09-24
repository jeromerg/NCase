using System;
using JetBrains.Annotations;
using NCase.Back.Api.Core;
using NCase.Back.Api.Parse;
using NDsl.Api.Core;


namespace NCase.Front.Imp
{
    public class DefHelperFactory : IDefHelperFactory
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly ISetFactory mSetFactory;
        [NotNull] private readonly IParserGenerator mParserGenerator;
        private readonly ICaseFactory mCaseFactory;

        public DefHelperFactory([NotNull] IParserGenerator parserGenerator,
                                [NotNull] ICodeLocationUtil codeLocationUtil,
                                [NotNull] ISetFactory setFactory,
                                [NotNull] ICaseFactory caseFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (setFactory == null) throw new ArgumentNullException("setFactory");
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");

            mParserGenerator = parserGenerator;
            mCodeLocationUtil = codeLocationUtil;
            mSetFactory = setFactory;
            mCaseFactory = caseFactory;
        }

        public DefHelper<TDefId> CreateDefHelper<TDefId>(TDefId def, string defName, ITokenReaderWriter tokenReaderWriter)
            where TDefId : IDefId
        {
            return new DefHelper<TDefId>(def,
                                         defName,
                                         tokenReaderWriter,
                                         mCodeLocationUtil,
                                         mParserGenerator,
                                         mSetFactory,
                                         mCaseFactory);
        }
    }
}