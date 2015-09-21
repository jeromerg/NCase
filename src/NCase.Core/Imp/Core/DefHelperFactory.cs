using System;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core
{
    public class DefHelperFactory : IDefHelperFactory
    {
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly ISetFactory mSetFactory;
        [NotNull] private readonly IParserGenerator mParserGenerator;

        public DefHelperFactory([NotNull] IParserGenerator parserGenerator,
                                [NotNull] ICodeLocationUtil codeLocationUtil,
                                [NotNull] ISetFactory setFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (setFactory == null) throw new ArgumentNullException("setFactory");

            mParserGenerator = parserGenerator;
            mCodeLocationUtil = codeLocationUtil;
            mSetFactory = setFactory;
        }

        public DefHelper<TDef> CreateDefHelper<TDef>(TDef def, string defName, ITokenReaderWriter tokenReaderWriter)
            where TDef : IDef
        {
            return new DefHelper<TDef>(def, defName, tokenReaderWriter, mCodeLocationUtil, mParserGenerator, mSetFactory);
        }
    }
}