using System;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Pairwise
{
    public class PairwiseCaseSetFactory : ICaseSetFactory<IPairwise>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public PairwiseCaseSetFactory([NotNull] IParserGenerator parserGenerator, [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mParserGenerator = parserGenerator;
            mCodeLocationUtil = codeLocationUtil;
        }

        public IPairwise Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
        {
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new PairwiseCaseSet(mParserGenerator, tokenReaderWriter, name, mCodeLocationUtil);
        }
    }
}