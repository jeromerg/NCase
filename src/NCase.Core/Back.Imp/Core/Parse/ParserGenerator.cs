using System;
using System.Collections.Generic;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Pub;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Tok;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core.Parse
{
    public class ParserGenerator : IParserGenerator
    {
        [NotNull] private readonly ICaseFactory mCaseFactory;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IGenerateDirector mCaseGenerator;
        [NotNull] private readonly IParseDirector mParseDirector;

        public ParserGenerator([NotNull] ICaseFactory caseFactory,
                               [NotNull] ICodeLocationUtil codeLocationUtil,
                               [NotNull] IGenerateDirector caseGenerator,
                               [NotNull] IParseDirector parseDirector)
        {
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (caseGenerator == null) throw new ArgumentNullException("caseGenerator");
            if (parseDirector == null) throw new ArgumentNullException("parseDirector");
            mCaseFactory = caseFactory;
            mCodeLocationUtil = codeLocationUtil;
            mCaseGenerator = caseGenerator;
            mParseDirector = parseDirector;
        }

        public IEnumerable<ICase> ParseAndGenerate(IDef def, ITokenReader tokenReader)
        {
            // PARSE
            foreach (IToken token in tokenReader.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            var reference = mParseDirector.GetReference<INode>(def, mCodeLocationUtil.GetCurrentUserCodeLocation());

            // WRAP RESULT INTO ICase
            foreach (List<INode> testCaseNodes in mCaseGenerator.Visit(reference))
                yield return mCaseFactory.Create(testCaseNodes);
        }
    }
}