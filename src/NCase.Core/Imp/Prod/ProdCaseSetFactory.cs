using System;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Prod
{
    public class ProdCaseSetFactory : ICaseSetFactory<IProd>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;

        public ProdCaseSetFactory([NotNull] IParserGenerator parserGenerator, [NotNull] ICodeLocationUtil codeLocationUtil)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            mParserGenerator = parserGenerator;
            mCodeLocationUtil = codeLocationUtil;
        }

        public IProd Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
        {
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new ProdCaseSet(mParserGenerator, tokenReaderWriter, name, mCodeLocationUtil);
        }
    }
}