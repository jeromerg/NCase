using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Dev.Core;
using NCase.Api.Dev.Core.Parse;
using NCase.Api.Dev.Core.Replay;
using NDsl.Api.Dev.Core;
using NDsl.Api.Dev.Core.Nod;
using NDsl.Api.Dev.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Core.Parse
{
    public class ParserGenerator : IParserGenerator
    {
        [NotNull] private readonly ICaseFactory mCaseFactory;
        [NotNull] private readonly ICodeLocationUtil mCodeLocationUtil;
        [NotNull] private readonly IGenerateDirector mCaseGenerator;
        [NotNull] private readonly IReplayDirector mRePlayDirector;
        [NotNull] private readonly IParseDirector mParseDirector;

        public ParserGenerator([NotNull] ICaseFactory caseFactory,
                               [NotNull] ICodeLocationUtil codeLocationUtil,
                               [NotNull] IGenerateDirector caseGenerator,
                               [NotNull] IReplayDirector rePlayDirector,
                               [NotNull] IParseDirector parseDirector)
        {
            if (caseFactory == null) throw new ArgumentNullException("caseFactory");
            if (codeLocationUtil == null) throw new ArgumentNullException("codeLocationUtil");
            if (caseGenerator == null) throw new ArgumentNullException("caseGenerator");
            if (rePlayDirector == null) throw new ArgumentNullException("rePlayDirector");
            if (parseDirector == null) throw new ArgumentNullException("parseDirector");
            mCaseFactory = caseFactory;
            mCodeLocationUtil = codeLocationUtil;
            mCaseGenerator = caseGenerator;
            mRePlayDirector = rePlayDirector;
            mParseDirector = parseDirector;
        }

        public IEnumerable<ICase> ParseAndGenerate(ICaseSet caseSet, ITokenReader tokenReader)
        {
            // PARSE
            foreach (var token in tokenReader.Tokens)
                mParseDirector.Visit(token);

            // GENERATE CASES
            var reference = mParseDirector.GetReference<INode>(caseSet, mCodeLocationUtil.GetCurrentUserCodeLocation());

            // WRAP RESULT INTO ICase
            foreach (List<INode> testCaseNodes in mCaseGenerator.Visit(reference))
                yield return mCaseFactory.Create(testCaseNodes);
        }

    }
}
