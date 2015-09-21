using System;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Tree
{
    public class TreeDefFactory : IDefFactory<ITree>
    {
        [NotNull] private readonly IParserGenerator mParserGenerator;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        private readonly ISetFactory mSetFactory;

        public TreeDefFactory([NotNull] IParserGenerator parserGenerator, [NotNull] ICodeLocationUtil codeLocationUtil,
            [NotNull] ISetFactory setFactory)
        {
            if (parserGenerator == null) throw new ArgumentNullException("parserGenerator");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (setFactory == null) throw new ArgumentNullException("setFactory");
            mParserGenerator = parserGenerator;
            mCodeLocationUtil = codeLocationUtil;
            mSetFactory = setFactory;
        }

        public ITree Create([NotNull] ITokenReaderWriter tokenReaderWriter, [NotNull] string name)
        {
            if (tokenReaderWriter == null) throw new ArgumentNullException("tokenReaderWriter");
            if (name == null) throw new ArgumentNullException("name");
            return new Tree(mParserGenerator, tokenReaderWriter, name, mCodeLocationUtil, mSetFactory);
        }
    }
}